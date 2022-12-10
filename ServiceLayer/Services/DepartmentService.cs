using System;
using DomainLayer.Entities;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories;
using ServiceLayer.Helpers;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
	public class DepartmentService : IDepartmentService
	{
        private readonly DepartmentRepository _DepartRepo;
        private static int _cnt = 1;

        public DepartmentService()
        {
            _DepartRepo = new DepartmentRepository();
        }

        public Department Create(Department? department)
        {
            department.Id = _cnt;
            _DepartRepo.Add(department);
            _cnt++;
            return department;
        }

        public void Update(int? id, Department department)
        {
            department.Id = id;
            _DepartRepo.Update(department);
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Department department = GetDepByID(id);

            if (department is null) throw new NotFoundException(ErrorMessage.DepNotFound);
            _DepartRepo.Delete(department);
        }

        public Department GetDepByID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            var res = _DepartRepo.Get(n => n.Id == id);
            return res;
        }

        public List<Department> GetAll()
        {
            return _DepartRepo.GetAll(null);
        }

        public List<Department> Search(string? word)
        {
            return _DepartRepo.GetAll(n => n.Name.ToLower().Contains(word.ToLower()));
        }
    }
}