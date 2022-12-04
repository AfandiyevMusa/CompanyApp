using System;
using DomainLayer.Entities;
using ServiceLayer.Helpers;
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
				string name = Console.ReadLine();

				ConsoleColor.Yellow.WriteWithColor("Enter department capacity: ");
                Capacity:  string depCapacity = Console.ReadLine();

				int newDepCapacity;

				bool isParse = int.TryParse(depCapacity, out newDepCapacity);

				if (isParse)
				{
					Department department = new()
					{
						Name = name,
						Capacity = newDepCapacity
					};

					var result = _departmentService.Creat(department);

                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Capacity: {result.Capacity}");
                }
				else
				{
					ConsoleColor.DarkRed.WriteWithColor("Please, add correct capacity: ");
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

		}

		public void Delete() //(3)
		{
			try
			{
                ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
				ID:  string depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

				if (isParse)
				{
					_departmentService.Delete(newDepID);
					ConsoleColor.Green.WriteWithColor("Deleted!!!");
				}
				else
				{
                    ConsoleColor.DarkRed.WriteWithColor("Please, add avaliable ID: ");
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
				ID: string depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

                if (isParse)
                {
					var result = _departmentService.GetDepByID(newDepID);
					if(result is null)
					{
						ConsoleColor.DarkRed.WriteWithColor("Department not found!!!");
						goto ID;
					}

                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Seat count: {result.Capacity}");
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor("Please, add avaliable ID: ");
                    goto ID;
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void GetAll()
		{
            ConsoleColor.Yellow.WriteWithColor("All datas: ");
			var result = _departmentService.GetAll();

			if(result is null)
			{
                ConsoleColor.DarkRed.WriteWithColor("Database is empty!!!");
            }

			foreach (var department in result)
			{
                ConsoleColor.Green.WriteWithColor($"Id: {department.Id}, Name: {department.Name}, Capacity: {result.Capacity}");
            }
        }
	}
}

