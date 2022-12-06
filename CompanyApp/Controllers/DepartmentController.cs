﻿using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
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

                Department department = new()
                {
                    Name = name,
                    Capacity = newDepCapacity
                };

                if (isParse)
				{
					var result = _departmentService.Create(department);

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
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
				ID: string depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

                ConsoleColor.Yellow.WriteWithColor("Enter new department Name: ");
				string name = Console.ReadLine();

                ConsoleColor.Yellow.WriteWithColor("Enter new department Capacity: ");
				NewCapacity: string updatedCapacity = Console.ReadLine();
				int newUpdatedCapacity;
				bool isParseID = int.TryParse(updatedCapacity, out newUpdatedCapacity);

                if (isParse)
                {
					if (isParseID)
					{
                        Department department = new()
                        {
                            Name = name,
                            Capacity = newUpdatedCapacity
                        };
						if (department is null) throw new ArgumentNullException();
                        var res = _departmentService.Update(newDepID, department);
						
                        if (res is null) throw new ArgumentNullException();
						ConsoleColor.Cyan.WriteWithColor("Updated!!!");
                    }
					else
					{
                        ConsoleColor.DarkRed.WriteWithColor("Please, add new ID which is avaliable: ");
                        goto NewCapacity;
                    }
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
				ID: string? depID = Console.ReadLine();
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

                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Capacity: {result.Capacity}");
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

		public void GetAll() //(5)
		{
            ConsoleColor.Yellow.WriteWithColor("All datas: ");
			var result = _departmentService.GetAll();

			if(result is null)
			{
                ConsoleColor.DarkRed.WriteWithColor("Database is empty!!!");
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
                string text = Console.ReadLine();

                var res = _departmentService.Search(text);

                foreach (var department in res)
                {
                        ConsoleColor.Green.WriteWithColor($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }

        //public Department GET() //(4)
        //{
        //    try
        //    {
        //        ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
        //    ID: string? depID = Console.ReadLine();
        //        int newDepID;
        //        bool isParse = int.TryParse(depID, out newDepID);

        //        if (isParse)
        //        {
        //            var result = _departmentService.GetDepByID(newDepID);
        //            if (result is null)
        //            {
        //                ConsoleColor.DarkRed.WriteWithColor("Department not found!!!");
        //                goto ID;
        //            }

        //            ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Capacity: {result.Capacity}");
        //        }
        //        else
        //        {
        //            ConsoleColor.DarkRed.WriteWithColor("Please, add avaliable ID: ");
        //            goto ID;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}


    }
}

