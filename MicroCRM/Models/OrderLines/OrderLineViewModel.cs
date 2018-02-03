using System;
using MicroCRM.Models.Products;

namespace MicroCRM.Models.OrderLines
{
    public class OrderLineViewModel
    {
        #region Public properties

        public Guid? Id { get; set; }

        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }

        public decimal? Total => Product?.Price * Quantity ?? null;
        
        #endregion
    }
}
