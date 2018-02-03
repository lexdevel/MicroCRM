using System;

namespace MicroCRM.Models.Customers
{
    public class CustomerViewModel
    {
        #region Public properties

        public Guid? Id { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        #endregion
    }
}
