using System;

namespace MicroCRM.Models.Users
{
    public class UserViewModel
    {
        #region Public properties

        public Guid? Id { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        #endregion
    }
}
