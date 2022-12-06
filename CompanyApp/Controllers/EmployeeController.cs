using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace CompanyApp.Controllers
{
	public class EmployeeController
	{
        private readonly DepartmentController _depController;
        private readonly DepartmentService _depService;
        private readonly EmployeeService _empService;

        public EmployeeController()
        {
            _empService = new EmployeeService();
        }

        public void Create() //(7)
        {
            try
            {
                if (AppDbContext<Department>.values.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter employee name: ");
                    string? name = Console.ReadLine();

                    ConsoleColor.Yellow.WriteWithColor("Enter employee surname: ");
                    string? surname = Console.ReadLine();

                    ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
                Age: string? age = Console.ReadLine();

                    int newAge;
                    bool isParse = int.TryParse(age, out newAge);

                    ConsoleColor.Yellow.WriteWithColor("Enter employee address: ");
                    string? address = Console.ReadLine();

                    ConsoleColor.Yellow.WriteWithColor("Enter employee department ID: ");
                    string? id = Console.ReadLine();

                    int newID;
                    bool isParseID = int.TryParse(id, out newID);

                    Department department = _depService.GetDepByID(newID);


                    Employee employee = new()
                    {
                        Name = name,
                        Surname = surname,
                        Age = newAge,
                        Address = address,
                        Department = department
                    };

                    if (isParse)
                    {
                        if (isParseID)
                        {
                            var result = _empService.Create(employee);
                            ConsoleColor.Green.WriteWithColor($"" +
                                $"Id: {result.Id}, " +
                                $"Name: {result.Name}, " +
                                $"Surname: {result.Surname}, " +
                                $"Age: {result.Age}, " +
                                $"Adress: {result.Address}, " +
                                $"Department: {result.Department.Name}");
                        }
                        else
                        {
                            ConsoleColor.DarkRed.WriteWithColor("Please, add correct ID: ");
                        }
                        
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor("Please, add correct age: ");
                        goto Age;
                    }
                }
                else
                {
                    throw new NotFoundException("Department not found!");                   
                }

            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        //public void GetAllByID() //(9)
        //{
        //    try
        //    {
        //        ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
        //    ID: string depID = Console.ReadLine();
        //        int newDepID;
        //        bool isParse = int.TryParse(depID, out newDepID);

        //        if (isParse)
        //        {
        //            var result = _empService.GetDepByID(newDepID);
        //            if (result is null)
        //            {
        //                ConsoleColor.DarkRed.WriteWithColor("Employee not found!!!");
        //                goto ID;
        //            }

        //            ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Surname: {result.Surname}, Address: {result.Address}");
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

        public void GetAllByAge() //(9)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
            Age: string empAge = Console.ReadLine();
                int newEmpAge;
                bool isParse = int.TryParse(empAge, out newEmpAge);

                if (isParse)
                {
                    var result = _empService.GetAllEmpByAge(newEmpAge);
                    if (result is null)
                    {
                        ConsoleColor.DarkRed.WriteWithColor("Employee not found!!!");
                        goto Age;
                    }
                    foreach (var eachEmp in result)
                    {
                        ConsoleColor.Green.WriteWithColor($"" +
                            $"Id: {eachEmp.Id}, " +
                            $"Name: {eachEmp.Name}, " +
                            $"Surname: {eachEmp.Surname}, " +
                            $"Age: {eachEmp.Age}, " +
                            $"Address: {eachEmp.Address}, " +
                            $"Department: {eachEmp.Department}");
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor("Please, add avaliable ID: ");
                    goto Age;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

