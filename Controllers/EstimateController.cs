using ecommerce.Data;
using ecommerce.DTO;
using Microsoft.AspNetCore.Mvc;


namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimateController : ControllerBase
    {
        private readonly EstimateService _service;

        public EstimateController(EstimateService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstimateDto dto)
        {
            
            await _service.CreateAsync(dto);

            return Ok(new {message = "Devis cree avec succes"});
        }
        


    }
}
