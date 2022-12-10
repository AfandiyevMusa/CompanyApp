﻿using System;
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
            if (newDepartment.Name == string.Empty)
            {
                newDepartment.Name = department.Name;
            }
            else
            {
                department.Name = newDepartment.Name;
            }
            
            if(newDepartment.Capacity == int.Parse(string.Empty))
            {
                Console.WriteLine(department.Capacity);
            }
            else
            {
                newDepartment.Capacity = department.Capacity;
            }
        }
    }
}