using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories;
using ServiceLayer.Helpers;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _empRepo;
        private readonly DepartmentRepository _depRepo;
        private int _cnt = 1;

        public EmployeeService()
        {
            _empRepo = new EmployeeRepository();
            _depRepo = new DepartmentRepository();
        }

        public Employee Create(Employee employee)
        {
            employee.Id = _cnt;
            _empRepo.Add(employee);
            _cnt++;
            return employee;
        }

        public List<Employee> Update(int id, Employee employee)
        {
            var res = _empRepo.Update(employee);

            foreach (var eachEmp in res)
            {
                if (eachEmp.Id == id)
                {
                    eachEmp.Name = employee.Name;
                    eachEmp.Surname = employee.Surname;
                    eachEmp.Age = employee.Age;
                    eachEmp.Address = employee.Address;
                    eachEmp.Department = employee.Department;
                    break;
                }
            }
            return res;
        }

        public Employee GetEmpByID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _empRepo.Get(n => n.Id == id);
        }

        public void Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            Employee employee = GetEmpByID(id);

            if (employee is null) throw new NotFoundException(ErrorMessage.EmpNotFound);
            _empRepo.Delete(employee);
        }

        public List<Employee> GetAllEmpByAge(int? age)
        {
            if (age is null) throw new ArgumentNullException();
            return _empRepo.GetAll(n => n.Age == age);
        }

        public List<Employee> GetAllEmpByID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _empRepo.GetAll(n => n.Id == id);
        }

        public List<Employee> GetAllEmpByDepID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _empRepo.GetAll(m => m.Department.Id == id);
        }

        public List<Employee> GetAllEmpByDepName(string? name)
        {
            if (name is null) throw new ArgumentNullException();
            return _empRepo.GetAll(m => m.Department.Name == name);
        }

        public List<Employee> Search(string? searchName, string searchSurname)
        {
            return _empRepo.GetAll(n => n.Name.ToLower().Contains(searchName.ToLower()) || n.Surname.ToLower().Contains(searchSurname.ToLower()));
        }

        public int GetAllEmpCount()
        {
            return _empRepo.GetAll(null).Count;
        }
    }
}