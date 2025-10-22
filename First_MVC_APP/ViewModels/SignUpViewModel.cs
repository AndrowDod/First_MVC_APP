using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "username is required!!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First name is required!!")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required!!")]
        public string LName { get; set; }

        [Required(ErrorMessage = "email is required!!")]
        [EmailAddress(ErrorMessage = "UnValid Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "password is required!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirm Password is required!!")]
        [Compare(nameof(Password), ErrorMessage ="confirm password is not match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
