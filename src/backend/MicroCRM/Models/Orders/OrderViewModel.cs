using System;
using System.Linq;
using System.Collections.Generic;
using MicroCRM.Models.Customers;
using MicroCRM.Models.OrderLines;

namespace MicroCRM.Models.Orders
{
    public class OrderViewModel
    {
        #region Public properties

        public Guid? Id { get; set; }

        public string OrderStatus { get; set; }

        public string OrderNumber { get; set; }

        public CustomerViewModel Customer { get; set; }

        public string Date { get; set; }
        
        public IEnumerable<OrderLineViewModel> OrderLines { get; set; }

        public decimal? Total => OrderLines?.Sum(ol => ol.Total) ?? null;

        #endregion
    }
}
