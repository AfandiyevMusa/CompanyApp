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

        public void Update(Employee newEmp)
        {
            var employee = Get(n => n.Id == newEmp.Id);

            if (newEmp.Name == string.Empty)
            {
                newEmp.Name = employee.Name;
            }
            else
            {
                employee.Name = newEmp.Name;
            }

            if (newEmp.Surname == string.Empty)
            {
                newEmp.Surname = employee.Surname;
            }
            else
            {
                employee.Surname = newEmp.Surname;
            }

            if (newEmp.Age == 0)
            {
                newEmp.Age = employee.Age;
            }
            else
            {
                employee.Age = newEmp.Age;
            }

            if (newEmp.Address == string.Empty)
            {
                newEmp.Address = employee.Address;
            }
            else
            {
                employee.Address = newEmp.Address;
            }

            if (newEmp.Department == null)
            {
                newEmp.Department = employee.Department;
            }
            else
            {
                employee.Department = newEmp.Department;
            }
        }
    }
}