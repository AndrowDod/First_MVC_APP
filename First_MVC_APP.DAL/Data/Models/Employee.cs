using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_APP.DAL.Data.Models
{
    public class Employee : ModelBase
    {
        public String Name { get; set; } 

        public int? Age { get; set; }

        public String Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; } 

        public bool IsDeleted { get; set; } = false;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public String ImageName { get; set; }

        // Navigation Property
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
