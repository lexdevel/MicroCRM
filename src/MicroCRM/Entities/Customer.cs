using System.ComponentModel.DataAnnotations.Schema;
using MicroCRM.Entities.Abstract;

namespace MicroCRM.Entities
{
    [Table("Customers")]
    public class Customer: LinkedEntity
    {
        #region Public properties

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }
        
        #endregion
    }
}
