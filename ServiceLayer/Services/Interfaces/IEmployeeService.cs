using System;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
	public interface IEmployeeService
	{
        Employee Create(Employee employee);
        Employee Update(int id, Employee employee);
        void Delete(int id);
        Employee GetDepByID(int id);
        List<Employee> GetAllEmpByAge(int? age);
        List<Employee> GetAllEmpByDepID(int? id);
        List<Employee> GetAllEmpByDepName(string depName);
        List<Employee> Search(string searchName, string searchSurname);
    }
}

