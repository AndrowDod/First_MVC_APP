using First_MVC_APP.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace First_MVC_APP.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required!!")]
        public String Code { get; set; }
        [Required(ErrorMessage = "Name is required!!")]
        public String Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
