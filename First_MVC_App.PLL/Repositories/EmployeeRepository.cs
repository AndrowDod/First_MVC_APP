using First_MVC_App.PLL.Interfaces;
using First_MVC_APP.DAL.Data;
using First_MVC_APP.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_App.PLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext _dbContext) : base(_dbContext)
        {
        }

        public IEnumerable<Employee> GetEmployeesByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> SearchByName(string searchInput)
        {
            return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(searchInput));
        }
    }
}
