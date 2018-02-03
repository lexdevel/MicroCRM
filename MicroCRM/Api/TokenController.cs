using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MicroCRM.Data;
using MicroCRM.Models.Token;
using MicroCRM.Services.Encryption;

namespace MicroCRM.Api
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class TokenController : Controller
    {
        #region Private members

        private readonly DataContext _dataContext;
        private readonly IEncryptionService _encryptionService;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataContext">The database context.</param>
        /// <param name="encryptionService">The encryption service.</param>
        public TokenController(DataContext dataContext, IEncryptionService encryptionService)
        {
            _dataContext = dataContext;
            _encryptionService = encryptionService;
        }

        #region Actions

        [HttpPost]
        public IActionResult Token([FromBody] SignInViewModel vm)
        {
            var user = _dataContext.Users.FirstOrDefault(us => us.Username == vm.Username);
            if (user == null)
            {
                return NotFound();
            }

            var passwordHash = _encryptionService.ComputeHexString(vm.Password);
            if (user.Password != passwordHash)
            {
                return Forbid();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Defaults.JwtSecret)), SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(Defaults.JwtIssuer, Defaults.JwtIssuer, claims, null, DateTime.Now.AddMinutes(Defaults.TokenValidTimeoutMinutes), signingCredentials);

            var response = new TokenViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Role = user.Role.ToString()
            };

            return Ok(response);
        }

        #endregion
    }
}
