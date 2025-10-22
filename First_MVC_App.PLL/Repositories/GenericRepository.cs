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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
   
            //create db context object
            private protected readonly AppDbContext _dbContext;

            public GenericRepository(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public void Add(T T)
               => _dbContext.Add(T);

            public void Delete(T T)
                => _dbContext.Remove(T);

            public IEnumerable<T> GetAll()
            {
            if (typeof(T) == typeof(Employee))

                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).ToList();
            else
                return _dbContext.Set<T>().ToList();
            }

            public T GetById(int id)
            {
                return _dbContext.Set<T>().Find(id);
            }

            public void Update(T T)
                => _dbContext.Update(T);
        }
}
