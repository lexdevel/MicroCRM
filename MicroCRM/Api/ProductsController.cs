using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroCRM.Data;
using MicroCRM.Entities;
using MicroCRM.Models.Products;
using MicroCRM.Sessions;

namespace MicroCRM.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ProductsController : Controller
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
        public ProductsController(DataContext dataContext, ISessionManager sessionManager)
        {
            _dataContext = dataContext;
            _sessionManager = sessionManager;
        }

        #region Actions

        [HttpGet]
        public IActionResult Get(int offset = 0, int length = Defaults.PageSize)
        {
            var vm = _dataContext.Products
                .OrderByDescending(pr => pr.CreatedAt)
                .Skip(offset)
                .Take(length)
                .Select(pr => new ProductViewModel
                {
                    Id = pr.Id,
                    Name = pr.Name,
                    CostPrice = pr.CostPrice,
                    Price = pr.Price
                });

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _dataContext.Products.FirstOrDefault(pr => pr.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CostPrice = product.CostPrice,
                Price = product.Price
            };

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductViewModel vm)
        {
            var product = new Product
            {
                Name = vm.Name,
                CostPrice = vm.CostPrice,
                Price = vm.Price
            };

            await _dataContext.Products.AddAsync(product).ConfigureAwait(false);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            return Get(product.Id);
        }

        #endregion
    }
}
