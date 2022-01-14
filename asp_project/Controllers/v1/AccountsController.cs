using asp_project.Helpers;
using asp_project.Models;
using asp_project.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace asp_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        public readonly ApplicationContext _context;
        public AccountsController(ApplicationContext context)
        {
            _context = context;
        }

        [EnableCors()]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            var identity = GetIdentity(user.Email, user.Password);

            if (identity == null)
                return BadRequest(new { errorText = "Invalid Email or Password!" });

            // Создание JWT-токена
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.Now,
                    claims: identity.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = await Task.Run(() => new
            {
                Token = encodedJwt,
                UserId = _context.Users.FirstOrDefault(x => x.Email == user.Email).Id,
                Email = identity.Name,
                Role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault(),
            });

            await _context.SaveChangesAsync();

            return Ok(response);
        }
        private ClaimsIdentity GetIdentity(string email, string password)
        {
            // Проверка на наличие пользователя с такой почтой
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.Roles.FirstOrDefault(x => x.Id == user.RoleId).Name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

    }
}
