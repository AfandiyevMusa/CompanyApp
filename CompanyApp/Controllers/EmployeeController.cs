using System;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace CompanyApp.Controllers
{
	public class EmployeeController
	{
        private readonly EmployeeService _empService;

        public EmployeeController()
        {
            _empService = new EmployeeService();
        }

        public void Create() //(7)
        {
            try
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

                //ConsoleColor.Yellow.WriteWithColor("Enter employee department: ");
                //Department? department = get
                

                Employee employee = new()
                {
                    Name = name,
                    Surname = surname,
                    Age = newAge,
                    Address = address,
                    //Department = AppDbContext<Department>.values
                };

                if (isParse)
                {
                    var result = _empService.Create(employee);
                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Surname: {result.Surname}, Age: {result.Age}, Adress: {result.Address}, Department: ");
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor("Please, add correct age: ");
                    goto Age;
                }

            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void GetAllByID() //(9)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
            ID: string depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

                if (isParse)
                {
                    var result = _empService.GetDepByID(newDepID);
                    if (result is null)
                    {
                        ConsoleColor.DarkRed.WriteWithColor("Employee not found!!!");
                        goto ID;
                    }

                    ConsoleColor.Green.WriteWithColor($"Id: {result.Id}, Name: {result.Name}, Surname: {result.Surname}, Address: {result.Address}");
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
    }
}

