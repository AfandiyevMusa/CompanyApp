using System;
using DomainLayer.Entities;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories
{
	public class EmployeeRepository : IRepository<Employee>
    {
        public void Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Employee Get(Predicate<Employee> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll(Predicate<Employee> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Department> Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}

