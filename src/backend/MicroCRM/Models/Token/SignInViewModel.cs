using System.ComponentModel.DataAnnotations;

namespace MicroCRM.Models.Token
{
    public class SignInViewModel
    {
        #region Public properties

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #endregion
    }
}
