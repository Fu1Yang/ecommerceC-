using ecommerce.DTO;
using ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RdvController : ControllerBase
    {
        private readonly RdvService _rdv;

        public RdvController(RdvService rdv)
        {
            _rdv = rdv;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RdvDto dto)
        {
            await _rdv.CreateAsync(dto);
            return Ok(new { message = "Rendez-vous créé avec succès" });
        }
    }
}
