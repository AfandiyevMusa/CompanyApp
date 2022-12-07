using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories
{
	public class EmployeeRepository : IRepository<Employee>
    {
        public void Add(Employee entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Employee>.datas.Add(entity);
        }

        public void Delete(Employee entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Employee>.datas.Remove(entity);
        }

        public Employee Get(Predicate<Employee> predicate)
        {
            return AppDbContext<Employee>.datas.Find(predicate);
        }

        public List<Employee> GetAll(Predicate<Employee> predicate)
        {
            return predicate == null ? AppDbContext<Employee>.datas : AppDbContext<Employee>.datas.FindAll(predicate);
        }

        public List<Department> Update(Employee entity)
        {
            return AppDbContext<Department>.datas;
        }
    }
}

