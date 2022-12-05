using System;
using DomainLayer.Entities;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
	public class DepartmentService : IDepartmentService
	{
        private readonly DepartmentRepository _DepartRepo;
        private static int _cnt;

        public DepartmentService()
        {
            _DepartRepo = new DepartmentRepository();
        }

        public Department Creat(Department department)
        {
            department.Id = _cnt;
            _DepartRepo.Add(department);
            _cnt++;
            return department;
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Department department = GetDepByID(id);

            if (department is null) throw new NotFoundException("Department not found");
            _DepartRepo.Delete(department);
        }

        public List<Department> GetAll()
        {
            return _DepartRepo.GetAll(null);
        }

        public Department GetDepByID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _DepartRepo.Get(n => n.Id == id);
        }

        public List<Department> Search(string word)
        {
            return _DepartRepo.GetAll(n => n.Name.ToLower().Contains(word.ToLower()));
        }

        public List<Department> Update(int id, Department department)
        {
            var res = _DepartRepo.Update(department);

            foreach (var eachDepartment in res)
            {
                if(eachDepartment.Id == id)
                {
                    eachDepartment.Name = department.Name;
                    eachDepartment.Capacity = department.Capacity;
                }
                else
                {
                    
                }
            }
            return res;
            
        }
    }
}

