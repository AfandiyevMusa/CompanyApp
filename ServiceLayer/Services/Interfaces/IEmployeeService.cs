﻿using System;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
	public interface IEmployeeService
	{
        Employee Create(Employee employee);
        Employee Update(int id, Employee employee);
        Employee GetEmpByID(int? id);
        void Delete(int? id);
        List<Employee> GetAllEmpByAge(int? age);
        List<Employee> GetAllEmpByDepID(int? id);
        List<Employee> GetAllEmpByDepName(string depName);
        List<Employee> Search(string searchName, string searchSurname);
        //bir de GET ALL EMPLOYEES COUNT
    }
}

