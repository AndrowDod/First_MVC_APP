using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_APP.DAL.Data.Models
{
    public class Department : ModelBase
    {

        public String Code { get; set; }
        public String Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
