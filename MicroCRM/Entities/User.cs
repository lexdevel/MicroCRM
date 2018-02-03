using MicroCRM.Entities.Abstract;

namespace MicroCRM.Entities
{
    public class User : AbstractEntity
    {
        #region Public properties

        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; } = Role.Sales;

        #endregion
    }
}
