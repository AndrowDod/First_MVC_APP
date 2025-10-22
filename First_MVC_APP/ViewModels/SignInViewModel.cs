using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "email is required!!")]
        [EmailAddress(ErrorMessage = "UnValid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
