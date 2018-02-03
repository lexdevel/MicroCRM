using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroCRM.Data;
using MicroCRM.Entities;
using MicroCRM.Models.Customers;
using MicroCRM.Sessions;

namespace MicroCRM.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class CustomersController : Controller
    {
        #region Private members

        private readonly DataContext _dataContext;
        private readonly ISessionManager _sessionManager;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataContext">The database context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public CustomersController(DataContext dataContext, ISessionManager sessionManager)
        {
            _dataContext = dataContext;
            _sessionManager = sessionManager;
        }

        #region Actions

        [HttpGet]
        public IActionResult Get(int offset = 0, int length = Defaults.PageSize)
        {
            var vm = _dataContext.Customers
                .OrderBy(cs => cs.Name)
                .Skip(offset)
                .Take(length)
                .Select(cs => new CustomerViewModel
                {
                    Id = cs.Id,
                    EmailAddress = cs.EmailAddress,
                    Name = cs.Name,
                    Gender = cs.Gender.ToString()
                });

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var customer = _dataContext.Customers.FirstOrDefault(cs => cs.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var vm = new CustomerViewModel
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                Name = customer.Name,
                Gender = customer.Gender.ToString()
            };

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModel vm)
        {
            var customer = new Customer
            {
                EmailAddress = vm.EmailAddress,
                Name = vm.Name,
                Gender = Enum.Parse<Gender>(vm.Gender, ignoreCase: true),
                CreatedById = _sessionManager.CurrentUser.Id,
                UpdatedById = _sessionManager.CurrentUser.Id
            };

            await _dataContext.Customers.AddAsync(customer).ConfigureAwait(false);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            return Get(customer.Id);
        }

        #endregion
    }
}
