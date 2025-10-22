using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "email is required!!")]
        [EmailAddress(ErrorMessage = "UnValid Email")]
        public string Email { get; set; }
    }
}
