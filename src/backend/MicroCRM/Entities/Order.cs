using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MicroCRM.Entities.Abstract;

namespace MicroCRM.Entities
{
    [Table("Orders")]
    public class Order : LinkedEntity
    {
        #region Public properties

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }
        
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<OrderLine> OrderLines { get; set; }

        #endregion
    }
}
