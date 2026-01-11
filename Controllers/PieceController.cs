using ecommerce.Data;
using ecommerce.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PieceController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        // GET: api/<PieceController>
        [HttpGet]
        public  async Task<IEnumerable<PieceDto>> Get()
        {
            try
            {
                return await _dataContext.Produits
                    .Select(p => new PieceDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImagePath = p.ImagePath
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching Piece: {ex.Message}");
                return Enumerable.Empty<PieceDto>();
            }
        }

        // GET api/<PieceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PieceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PieceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PieceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
