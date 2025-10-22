using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "password is required!!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!!")]
        [Compare(nameof(NewPassword), ErrorMessage = "confirm password is not match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
