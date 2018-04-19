using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroCRM.Auth;
using MicroCRM.Data;
using MicroCRM.Entities;
using MicroCRM.Models.Users;
using MicroCRM.Services.Encryption;
using MicroCRM.Services.Random;

namespace MicroCRM.Api
{
    [Authorize(Roles = nameof(Role.Admin))]
    public class UsersController : RestController
    {
        #region Prvate members

        private readonly IEncryptionService _encryptionService;
        private readonly IRangomService _randomService;
        
        #endregion

        public UsersController(AuthContext authContext, DataContext dataContext, IEncryptionService encryptionService, IRangomService randomService)
            : base(authContext, dataContext)
        {
            _encryptionService = encryptionService;
            _randomService = randomService;
        }

        #region Actions

        [HttpGet]
        public IActionResult Get(int offset = 0, int length = Defaults.PageSize)
        {
            var vm = _dataContext.Users
                .OrderBy(us => us.Username)
                .Skip(offset)
                .Take(length)
                .Select(us => new UserViewModel
                {
                    Id = us.Id,
                    Username = us.Username,
                    Role = us.Role.ToString()
                });

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel vm)
        {
            var user = _dataContext.Users.FirstOrDefault(us => us.Username == vm.Username);
            if (user != null)
            {
                return Forbid();
            }

            var username = vm.Username.EndsWith(Defaults.Domain) ? vm.Username : $"{vm.Username}@{Defaults.Domain}";
            var password = _randomService.GenerateRandomString(8);

            user = new User
            {
                Username = username,
                Password = _encryptionService.ComputeHexString(password),
                Role = Enum.Parse<Role>(vm.Role, ignoreCase: true)
            };

            await _dataContext.Users.AddAsync(user).ConfigureAwait(false);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            return Ok(new { username, password });
        }

        #endregion
    }
}
