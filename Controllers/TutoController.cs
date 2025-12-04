using ecommerce.DTO;
using ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutoController : ControllerBase
    {
        private readonly DataContext _context;

        public TutoController(DataContext context)
        {
            _context = context; // OBLIGATOIRE
        }
        // GET: api/<TutoController>
        [HttpGet]
        public async Task<IEnumerable<TutoDto>> GetTutos()
        {
            try
            {
                return await _context.Tuto
                 .Select(t => new TutoDto
                 {
                     Id = t.Id,
                     TitleTuto = t.TitleTuto,
                     Description = t.Description,
                     Url = t.Url
                 })
                 .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching tutos: {ex.Message}");
                return Enumerable.Empty<TutoDto>();
            }

        }

        // GET api/<TutoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TutoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TutoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TutoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
