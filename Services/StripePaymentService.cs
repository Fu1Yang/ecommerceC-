namespace ecommerce.Services
{
    using Stripe;
    public class StripePaymentService
    {
        public async Task<PaymentIntent> CreatePaymentIntentAsync(long amount, string currency = "eur")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount, // montant en centimes (ex: 1000 = 10€)
                Currency = currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }

        public async Task<Customer> CreateCustomerAsync(string email, string name)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Name = name,
            };

            var service = new CustomerService();
            return await service.CreateAsync(options);
        }
    }
}
