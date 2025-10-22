using First_MVC_App.PLL.Interfaces;
using First_MVC_APP.DAL.Data;
using First_MVC_APP.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_App.PLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext): base(dbContext)
        {
            
        }
        
    }
}
