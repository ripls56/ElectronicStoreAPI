using ElectroStoreAPI.Core;
using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public TokenController(ElectronicStoreContext context)
        {
            _context = context;
        }
        // POST: api/Token
        /// <summary>
        /// Получения access токена
        /// </summary>
        /// <param name="authParams"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Authorize([FromBody] AuthParams authParams)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(predicate: c => c.LoginClient == authParams.login).ConfigureAwait(false);
            if (client == null) return Results.NotFound();
            var pass = Helper.ToSha256(authParams.password + client.SaltClient).ToUpper();
            if (client.PasswordClient != pass) return Results.Unauthorized();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, client.LoginClient), new Claim(ClaimTypes.Role, "client") };
            var expiresIn = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1));
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: expiresIn,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expiresIn = ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(),
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

public partial class AuthParams
{
    public string? login { get; set; }
    public string? password { get; set; }

}
