using System;
using System.Text.RegularExpressions;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        public void Add(Department entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Department>.datas.Add(entity);
        }

        public void Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException();
            AppDbContext<Department>.datas.Remove(entity);
        }

        public Department Get(Predicate<Department> predicate)
        {
            return AppDbContext<Department>.datas.Find(predicate);
        }

        public List<Department> GetAll(Predicate<Department> predicate)
        {
            return predicate == null ? AppDbContext<Department>.datas : AppDbContext<Department>.datas.FindAll(predicate);
        }

        public void Update(Department newDepartment)
        {
            var department = Get(n => n.Id == newDepartment.Id);
            if (newDepartment.Name == string.Empty || Regex.IsMatch(newDepartment.Name, @"^[\s]+$"))
            {
                newDepartment.Name = department.Name;
            }
            else
            {
                department.Name = newDepartment.Name;
            }

            if (newDepartment.Capacity == 0)
            {
                newDepartment.Capacity = department.Capacity;
            }
            else
            {
                department.Capacity = newDepartment.Capacity;
            }
        }
    }
}