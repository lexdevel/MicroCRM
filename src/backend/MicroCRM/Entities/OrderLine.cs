using System;
using System.ComponentModel.DataAnnotations.Schema;
using MicroCRM.Entities.Abstract;

namespace MicroCRM.Entities
{
    [Table("OrderLines")]
    public class OrderLine : LinkedEntity
    {
        #region Public properties

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        #endregion
    }
}
