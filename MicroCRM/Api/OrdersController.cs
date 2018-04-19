using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroCRM.Auth;
using MicroCRM.Data;
using MicroCRM.Entities;
using MicroCRM.Models.Customers;
using MicroCRM.Models.Orders;
using MicroCRM.Models.OrderLines;
using MicroCRM.Models.Products;

namespace MicroCRM.Api
{
    [Authorize]
    public class OrdersController : RestController
    {
        public OrdersController(AuthContext authContext, DataContext dataContext)
            : base(authContext, dataContext)
        {
        }

        #region Actions

        [HttpGet]
        public IActionResult Get(int offset = 0, int length = Defaults.PageSize)
        {
            var vm = _dataContext.Orders
                .Include(or => or.Customer)
                .OrderByDescending(or => or.CreatedAt)
                .Skip(offset)
                .Take(length)
                .Select(or => new OrderViewModel
                {
                    Id = or.Id,
                    Date = or.CreatedAt.ToString(Defaults.DateFormat),
                    OrderStatus = or.OrderStatus.ToString().ToLowerInvariant(),
                    OrderNumber = or.OrderNumber.ToString(Defaults.OrderNumberFormat),
                    Customer = new CustomerViewModel
                    {
                        Id = or.Customer.Id,
                        EmailAddress = or.Customer.EmailAddress,
                        Name = or.Customer.Name,
                        Gender = or.Customer.Gender.ToString()
                    }
                });

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var order = _dataContext.Orders
                .Include(or => or.Customer)
                .Include(or => or.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(or => or.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var vm = new OrderViewModel
            {
                Id = order.Id,
                Date = order.UpdatedAt.ToString(Defaults.DateFormat),
                OrderStatus = order.OrderStatus.ToString().ToLowerInvariant(),
                OrderNumber = order.OrderNumber.ToString(Defaults.OrderNumberFormat),
                Customer = new CustomerViewModel
                {
                    Id = order.Customer.Id,
                    EmailAddress = order.Customer.EmailAddress,
                    Name = order.Customer.Name,
                    Gender = order.Customer.Gender.ToString()
                },
                OrderLines = order.OrderLines.Select(ol => new OrderLineViewModel
                {
                    Id = ol.Id,
                    Quantity = ol.Quantity,
                    Product = new ProductViewModel
                    {
                        Id = ol.Product.Id,
                        Name = ol.Product.Name,
                        CostPrice = ol.Product.CostPrice,
                        Price = ol.Product.Price
                    }
                })
            };

            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel vm)
        {
            var order = new Order
            {
                OrderStatus = OrderStatus.Created,
                OrderNumber = _dataContext.Orders.Count() + 1,
                CustomerId = vm.Customer.Id ?? throw new ArgumentNullException(nameof(vm.Customer.Id)),
                CreatedById = _authContext.CurrentUser.Id,
                UpdatedById = _authContext.CurrentUser.Id,
            };

            var lines = vm.OrderLines.Select(ol => new OrderLine
            {
                OrderId = order.Id,
                Quantity = ol.Quantity,
                ProductId = ol.Product.Id ?? throw new ArgumentNullException(nameof(ol.Product.Id)),
                CreatedById = _authContext.CurrentUser.Id,
                UpdatedById = _authContext.CurrentUser.Id
            });

            await _dataContext.Orders.AddAsync(order).ConfigureAwait(false);
            await _dataContext.OrderLines.AddRangeAsync(lines).ConfigureAwait(false);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            return Get(order.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OrderViewModel vm)
        {
            var order = _dataContext.Orders.FirstOrDefault(or => or.Id == vm.Id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = Enum.Parse<OrderStatus>(vm.OrderStatus, ignoreCase: true);
            order.UpdatedAt = DateTime.UtcNow;
            order.UpdatedById = _authContext.CurrentUser.Id;

            _dataContext.Orders.Update(order);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);

            return Get(order.Id);
        }

        #endregion
    }
}
