using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetProjectLibrary.Models;
using DotNetProjectAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNetProjectAPI.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService AuthenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticate a user with email and password
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="password">The user's password</param>
        /// <returns>A JWT token for authentication and the user's ID</returns>
        /// <exception cref="Unauthorized">Thrown when authentication failed</exception>
        [HttpGet("{email}/{password}")]
        public ActionResult<User> Authenticate(string email, string password)
        {
            User? user = AuthenticationService.Authenticate(email, password);

            if (user is null) return Unauthorized();

            string token = GenerateToken(email);

            JObject jsonObject = new JObject
            {
                { "user_id", user.id },
                { "token", token.ToString() }
                
            };

            return Ok(JsonConvert.SerializeObject(jsonObject));
        }

        private string GenerateToken(string email)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("RegisterAPI3iLpremierdecembre2023"));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                new Claim("email", email)
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken("3iL", "RegisterAPI", claims, DateTime.Now, DateTime.Now.AddHours(1), signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
