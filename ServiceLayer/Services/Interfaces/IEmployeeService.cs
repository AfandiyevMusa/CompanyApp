using System;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
	public interface IEmployeeService
	{
        Employee Create(Employee employee);
        void Update(int id, Employee employee);
        Employee GetEmpByID(int? id);
        void Delete(int? id);
        List<Employee> GetAllEmpByAge(int? age);
        List<Employee> GetAllEmpByID(int? id);
        List<Employee> GetAllEmpByDepID(int? id);
        List<Employee> GetAllEmpByDepName(string? name);
        List<Employee> Search(string searchName, string searchSurname);
        int GetAllEmpCount();
    }
}