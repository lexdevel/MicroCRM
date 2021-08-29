using System.ComponentModel.DataAnnotations.Schema;
using MicroCRM.Entities.Abstract;

namespace MicroCRM.Entities
{
    [Table("Products")]
    public class Product : AbstractEntity
    {
        #region Public properties

        public string Name { get; set; }
        
        public decimal CostPrice { get; set; }

        public decimal Price { get; set; }

        #endregion
    }
}
