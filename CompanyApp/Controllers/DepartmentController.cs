using System;
using System.Xml.Linq;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
using ServiceLayer.Helpers;
using ServiceLayer.Helpers.Constants;
using ServiceLayer.Services;

namespace CompanyApp.Controllers
{
	public class DepartmentController
	{
		private readonly DepartmentService _departmentService;

		public DepartmentController()
		{
			_departmentService = new DepartmentService();
		}

		public void Create() //(1)
		{
            try
			{
				ConsoleColor.Yellow.WriteWithColor("Enter department name: ");
				string? name = Console.ReadLine();

				ConsoleColor.Yellow.WriteWithColor("Enter department capacity: ");
                Capacity:  string? depCapacity = Console.ReadLine();

				int newDepCapacity;

				bool isParse = int.TryParse(depCapacity, out newDepCapacity);

                Department department = new()
                {
                    Name = name,
                    Capacity = newDepCapacity
                };

                if (isParse && newDepCapacity > 0)
				{
                    var result = _departmentService.Create(department);
                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Capacity: {result.Capacity}");
                }
				else
				{
					ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CapacityMessage);
					goto Capacity;
                }
            }
			catch (Exception ex)
			{
				ConsoleColor.DarkRed.WriteWithColor(ex.Message);
			}
		}

		public void Update() //(2)
        {
            try
            {
                if (AppDbContext<Department>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
                ID: string? depID = Console.ReadLine();
                    int newDepID;
                    bool isParse = int.TryParse(depID, out newDepID);

                    Department department = new();

                    if (isParse)
                    {
                        if(_departmentService.GetDepByID(newDepID) != null)
                        {
                            ConsoleColor.Yellow.WriteWithColor("Enter new department Name: ");
                            string? name = Console.ReadLine();
                            department.Name = name;

                            ConsoleColor.Yellow.WriteWithColor("Enter new department Capacity: ");
                        NewCapacity: string? updatedCapacity = Console.ReadLine();
                            int newUpdatedCapacity;
                            
                            if(updatedCapacity == "")
                            {
                                newUpdatedCapacity = 0;
                            }
                            else
                            {
                                bool isParseCap = int.TryParse(updatedCapacity, out newUpdatedCapacity);

                                if (isParseCap)
                                {
                                    
                                    department.Capacity = newUpdatedCapacity;

                                    if (department is null) throw new ArgumentNullException();
                                    
                                }
                                else
                                {
                                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NewCapacityMessage);
                                    goto NewCapacity;
                                }
                            }
                            _departmentService.Update(newDepID, department);
                            ConsoleColor.Cyan.WriteWithColor(ErrorMessage.Updated);
                        }
                        else
                        {
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.IdShouldBeNum);
                        goto ID;
                    }
                }
                else
                {
                    throw new NotFoundException(ErrorMessage.NoDepartment);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

		public void Delete() //(3)
		{
			try
			{
                ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
				ID:  string? depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

				if (isParse)
				{
                    if (_departmentService.GetDepByID(newDepID) != null)
                    {
                        _departmentService.Delete(newDepID);
                        ConsoleColor.Green.WriteWithColor(ErrorMessage.Deleted);
                    }
                    else
                    {
                        throw new NotFoundException(ErrorMessage.DepIdNotFound);
                    }
				}
				else
				{
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.IdShouldBeNum);
                    goto ID;
                }

            }
			catch (Exception ex)
			{
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
		}

		public void GetByID() //(4)
        {
			try
			{
				ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
				ID: string? depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

                if (isParse)
                {
                    if (_departmentService.GetDepByID(newDepID) != null)
                    {
                        var result = _departmentService.GetDepByID(newDepID);
                        if (result is null)
                        {
                            ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.DepNotFound);
                            goto ID;
                        }

                        ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Capacity: {result.Capacity}");
                    }
                    else
                    {
                        throw new NotFoundException(ErrorMessage.DepIdNotFound);
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.IdShouldBeNum);
                    goto ID;
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void GetAll() //(5)
		{
            ConsoleColor.Yellow.WriteWithColor("All datas: ");
			var result = _departmentService.GetAll();

			if(result is null)
			{
                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.DatabaseIsEmpty);
            }

			foreach (var department in result)
			{
                ConsoleColor.Green.WriteWithColor($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
            }
        }

		public void Search() //(6)
		{
			try
			{
                ConsoleColor.Yellow.WriteWithColor("Enter Department name: ");
                string? text = Console.ReadLine();

                var res = _departmentService.Search(text);
				if (res.Count != 0)
				{
                    foreach (var department in res)
                    {
                        ConsoleColor.Green.WriteWithColor($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
                    }
                }
				else
				{
					throw new NotFoundException(ErrorMessage.DepNotFound);
				}
            }
			catch (Exception ex)
			{
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
			}
        }
    }
}