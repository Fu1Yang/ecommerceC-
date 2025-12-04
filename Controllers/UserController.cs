using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private string email;
        private string password;

        public UserController(DataContext context)
        {
            _context = context; // OBLIGATOIRE
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("infoUser")]
        public async Task<ActionResult<string>> InfoUser([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Requete invalide"
                });

            if (string.IsNullOrEmpty(request.Id) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new LoginResponse { Success = false, Message = "Champs vides" });
            }
            //En 1 On cherche l'utilisateur par email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Id);

            if (user == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "Email incorrect" });
            //En 2 Vérification mot de passe avec hashing
            bool ValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!ValidPassword)
                return Unauthorized(new LoginResponse { Success = false, Message = "Mot de passe incorrect" });

            // Connexion OK
            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Connexion réussie"
            });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
