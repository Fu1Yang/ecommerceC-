namespace ecommerce.Controllers
{
    using ecommerce.Services;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using Stripe.Checkout;
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly StripePaymentService _paymentService;
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _paymentService = new StripePaymentService();
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
        {
            try
            {
                var paymentIntent = await _paymentService.CreatePaymentIntentAsync(
                    request.Amount,
                    request.Currency ?? "eur"
                );

                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Traiter le paiement réussi
                Console.WriteLine($"Paiement réussi: {paymentIntent?.Id}");
            }

            return Ok();
        }
    }

    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string? Currency { get; set; }
    }
}
