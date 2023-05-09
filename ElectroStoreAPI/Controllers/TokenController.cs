using ElectroStoreAPI.Core;
using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectroStoreAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly ElectronicStoreContext _context;

        public TokenController(ElectronicStoreContext context)
        {
            _context = context;
        }
        // POST: api/login
        [HttpPost]
        public async Task<IResult> Authorize(string login, string password)
        {
            Client client = _context.Clients.FirstOrDefault(predicate: c => c.LoginClient == login && c.PasswordClient == password);
            if (client == null) return Results.Unauthorized();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, client.LoginClient), };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = client.LoginClient
            };
            return Results.Json(response);
        }

        //// POST api/Token
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
    }
}
