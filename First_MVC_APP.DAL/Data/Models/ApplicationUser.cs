using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_APP.DAL.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String FName { get; set; }
        public String LName { get; set; }
        public bool IsAgree { get; set; }
    }
}
