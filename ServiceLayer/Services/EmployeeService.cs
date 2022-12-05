using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _empRepo;
        private int _cnt;

        public EmployeeService()
        {
            _empRepo = new EmployeeRepository();
        }

        public Employee Create(Employee employee)
        {
            //foreach (var eachDep in AppDbContext<Department>.values)
            //{
                //if (eachDep is null) throw new ArgumentNullException();
                employee.Id = _cnt;
                _empRepo.Add(employee);
                _cnt++;
                return employee;
            //}
            //return employee;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmpByAge(int? age)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmpByDepID(int? id)
        {
            if (id is null) throw new ArgumentNullException();
            return _empRepo.GetAll(n => n.Id == id);
        }

        public List<Employee> GetAllEmpByDepName(string depName)
        {
            throw new NotImplementedException();
        }

        public Employee GetDepByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> Search(string searchName, string searchSurname)
        {
            throw new NotImplementedException();
        }

        public Employee Update(int id, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}

