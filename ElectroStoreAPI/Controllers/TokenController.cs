using ElectroStoreAPI.Core;
using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
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
            var expiresIn = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));
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
            return Results.Json(response, statusCode: StatusCodes.Status200OK);
        }


        /// <summary>
        /// Получение access токена для сотрудников
        /// </summary>
        /// <param name="authParams"></param>
        /// <returns></returns>
        [Route("employee")]
        [HttpPost]
        public async Task<IResult> EmployeeAuthorize([FromBody] AuthParams authParams)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(predicate: c => c.LoginEmployee == authParams.login).ConfigureAwait(false);
            if (employee == null) return Results.NotFound();

            var pass = Helper.ToSha256(authParams.password + employee.SaltEmployee).ToUpper();
            if (employee.PasswordEmployee != pass) return Results.Unauthorized();

            var post = await _context.Posts.FindAsync(employee.PostId);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, employee.LoginEmployee), new Claim(ClaimTypes.Role, post.NamePost) };
            var expiresIn = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));
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
                username = employee.LoginEmployee
            };
            return Results.Json(response, statusCode: StatusCodes.Status200OK);
        }
    }
}

public partial class AuthParams
{

    public AuthParams(string login, string password)
    {
        this.login = login;
        this.password = password;
    }
    public string? login { get; set; }
    public string? password { get; set; }

}
