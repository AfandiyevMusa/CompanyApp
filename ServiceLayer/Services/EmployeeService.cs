using System;
using DomainLayer.Entities;
using RepositoryLayer.Repositories;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _empRepo;
        private int _cnt = 1;

        public EmployeeService()
        {
            _empRepo = new EmployeeRepository();
        }

        public Employee Creat(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmpByAge(int age)
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

