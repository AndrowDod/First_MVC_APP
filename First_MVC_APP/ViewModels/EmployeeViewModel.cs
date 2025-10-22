using First_MVC_APP.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required ! ")]
        [MaxLength(50, ErrorMessage = "Max Length of name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length of name is 5 chars")]
        public String Name { get; set; }

        public String ImageName { get; set; }
        [Required(ErrorMessage = "Image is required ! ")]
        public IFormFile Image { get; set; }


        [Range(22, 30)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,4}-[a-zA-Z0-9]{2,20}-[a-zA-Z0-9]{2,20}-[a-zA-Z0-9]{2,20}$"
                            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public String Address { get; set; }


        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [EmailAddress]
        public String Email { get; set; }


        [Display(Name = "Phone Number")]
        [Phone]
        public String PhoneNumber { get; set; }


        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        // Navigation Property
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
