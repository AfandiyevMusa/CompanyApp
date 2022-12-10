using System;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
	public interface IDepartmentService
	{
		Department Create(Department? department);
        void Update(int? id, Department department);
		void Delete(int? id);
		Department GetDepByID(int? id);
		List<Department> GetAll();
		List<Department> Search(string? word);
	}
}
