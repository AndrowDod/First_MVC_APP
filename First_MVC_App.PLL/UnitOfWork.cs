using First_MVC_App.PLL.Interfaces;
using First_MVC_App.PLL.Repositories;
using First_MVC_APP.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_MVC_App.PLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
        }
        public int Complete()
        {
            return _context.SaveChanges();  
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
