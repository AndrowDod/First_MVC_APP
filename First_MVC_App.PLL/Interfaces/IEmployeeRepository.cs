using First_MVC_App.PLL.Repositories;
using First_MVC_APP.DAL.Data;
using First_MVC_APP.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_App.PLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public IEnumerable<Employee> GetEmployeesByAddress(string address);
        public IQueryable<Employee> SearchByName(string searchInput);

    }
}
