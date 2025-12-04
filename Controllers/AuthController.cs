using ecommerce.Models;
using ecommerce.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce.Controllers
{
  
    //public class LoginInfo
    //{
    //    public string Id { get; set; }
    //    public string Password { get; set; }
    //}

    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        // POST: api/<AuthController>
        [HttpPost("loginAd")]
        public ActionResult<string> LoginAd([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Requete invalide"
                });

            if (string.IsNullOrEmpty(request.Id) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("champ vide");
            }

            var id = _configuration["Auth:AdminLogin"];
            var password = _configuration["Auth:AdminPassword"];
        
            if (request.Id == id && request.Password == password)
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Connexion réussie"
                });
            }
            else
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Identifications incorrects"
                });
            }

            //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[] {
            //        new Claim(ClaimTypes.Name, request.Id)
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(2),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }

        //[Authorize]
        //[HttpGet("secure-data")]
        //public IActionResult GetSecureData()
        //{
        //    return Ok("Cette donnée est protégée !");
        //}


        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
