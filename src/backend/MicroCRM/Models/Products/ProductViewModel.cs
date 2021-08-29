using System;
using System.ComponentModel.DataAnnotations;

namespace MicroCRM.Models.Products
{
    public class ProductViewModel
    {
        #region Public properties

        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        #endregion
    }
}
