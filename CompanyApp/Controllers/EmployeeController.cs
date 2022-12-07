using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DomainLayer.Entities;
using RepositoryLayer.Datas;
using RepositoryLayer.Exceptions;
using ServiceLayer.Helpers;
using ServiceLayer.Helpers.Constants;
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
            _depService = new DepartmentService();
        }

        public void Create() //(7)
        {
            try
            {
                if (AppDbContext<Department>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter employee name: ");
                Name: string? name = Console.ReadLine();
                    //@"!@#%ˆ&()-={}:;''<>?,./|˜`1234567890"
                    if ((!Regex.IsMatch(name, @"[0-9]")) &&
                        (!Regex.IsMatch(name, @"[A-Za-z0-9]")) &&
                        //(!Regex.IsMatch(name, @"[A-Za-z0-9 @ # $ % ˆ & * ( ) _ + = / . , < > ` ˜ \ |; : < > ? ! ]")) &&
                        //(!Regex.IsMatch(name, @"^[ @ # $ % ˆ & * ( ) _ + = / . , < > ` ˜ \ |; : < > ? ! ]*$")) &&
                        name != " ")
                    {
                        ConsoleColor.Yellow.WriteWithColor("Enter employee surname: ");
                        Surname: string? surname = Console.ReadLine();

                        if ((Regex.IsMatch(surname, "[A-z]")) && surname != " ")
                        {
                            ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
                        Age: string? age = Console.ReadLine();
                            int newAge;
                            bool isParse = int.TryParse(age, out newAge);

                            ConsoleColor.Yellow.WriteWithColor("Enter employee address: ");
                            string? address = Console.ReadLine();

                            ConsoleColor.Yellow.WriteWithColor("Enter employee department ID: ");
                        ID: string? id = Console.ReadLine();
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
                                        $"DepartmentName: {result.Department?.Name}");
                                }
                                else
                                {
                                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectID);
                                    goto ID;
                                }
                            }
                            else
                            {
                                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectAge);
                                goto Age;
                            }
                        }
                        else
                        {
                            ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectSurname);
                            goto Surname;
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectName);
                        goto Name;
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

        public void GetEmpByID() //(9)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
            ID: string? depID = Console.ReadLine();
                int newDepID;
                bool isParse = int.TryParse(depID, out newDepID);

                if (isParse)
                {
                    var result = _empService.GetEmpByID(newDepID);
                    if (result is null)
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.EmpNotFound);
                        goto ID;
                    }

                    ConsoleColor.Green.WriteWithColor($"" +
                                $"Id: {result.Id}, " +
                                $"Name: {result.Name}, " +
                                $"Surname: {result.Surname}, " +
                                $"Age: {result.Age}, " +
                                $"Adress: {result.Address}, " +
                                $"DepartmentName: {result.Department?.Name}");
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                    goto ID;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void Delete() //(10)
        {
            ConsoleColor.Yellow.WriteWithColor("Enter employee ID: ");
        ID: string? empID = Console.ReadLine();
            int newEmpID;
            bool isParse = int.TryParse(empID, out newEmpID);

            if (isParse)
            {
                _empService.Delete(newEmpID);
                ConsoleColor.Green.WriteWithColor("Deleted!!!");
            }
            else
            {
                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                goto ID;
            }
        }

        public void GetAllByAge() //(11)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
            Age: string? empAge = Console.ReadLine();
                int newEmpAge;
                bool isParse = int.TryParse(empAge, out newEmpAge);

                if (isParse)
                {
                    var result = _empService.GetAllEmpByAge(newEmpAge);
                    if (result is null)
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.EmpNotFound);
                        goto Age;
                    }
                    foreach (var eachEmp in result)
                    {
                        ConsoleColor.Green.WriteWithColor($"" +
                                $"Id: {eachEmp.Id}, " +
                                $"Name: {eachEmp.Name}, " +
                                $"Surname: {eachEmp.Surname}, " +
                                $"Age: {eachEmp.Age}, " +
                                $"Adress: {eachEmp.Address}, " +
                                $"DepartmentName: {eachEmp.Department?.Name}");
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                    goto Age;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllEmpByDepID() //(12)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter Employee department ID: ");
            ID: string? empID = Console.ReadLine();
                int newEmpID;
                bool isParse = int.TryParse(empID, out newEmpID);

                if (isParse)
                {
                    var res = _empService.GetAllEmpByDepID(newEmpID);
                    if (res.Count is 0) throw new NotFoundException(ErrorMessage.DepIdNotFound);
                    foreach (var eachEmp in res)
                    {
                        ConsoleColor.Green.WriteWithColor($"" +
                                    $"Id: {eachEmp.Id}, " +
                                    $"Name: {eachEmp.Name}, " +
                                    $"Surname: {eachEmp.Surname}, " +
                                    $"Age: {eachEmp.Age}, " +
                                    $"Adress: {eachEmp.Address}, " +
                                    $"DepartmentName: {eachEmp.Department?.Name}");
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                    goto ID;
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void GetAllEmpByDepName() //(13)
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter Employee department Name: ");
                string? depName = Console.ReadLine();

                var res = _empService.GetAllEmpByDepName(depName);
                if (res.Count is 0) throw new NotFoundException(ErrorMessage.DepNameNotFound);

                foreach (var eachEmp in res)
                {
                    ConsoleColor.Green.WriteWithColor($"" +
                                $"Id: {eachEmp.Id}, " +
                                $"Name: {eachEmp.Name}, " +
                                $"Surname: {eachEmp.Surname}, " +
                                $"Age: {eachEmp.Age}, " +
                                $"Adress: {eachEmp.Address}, " +
                                $"DepartmentName: {eachEmp.Department?.Name}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void Search()
        {
            try
            {
                ConsoleColor.Yellow.WriteWithColor("Enter employee name: ");
                string? name = Console.ReadLine();

                ConsoleColor.Yellow.WriteWithColor("Enter employee surname: ");
                string? surname = Console.ReadLine();

                var res = _empService.Search(name, surname);
                if (res.Count != 0)
                {
                    foreach (var employee in res)
                    {
                        ConsoleColor.Green.WriteWithColor($"" +
                                     $"Id: {employee.Id}, " +
                                     $"Name: {employee.Name}, " +
                                     $"Surname: {employee.Surname}, " +
                                     $"Age: {employee.Age}, " +
                                     $"Adress: {employee.Address}, " +
                                     $"DepartmentName: {employee.Department?.Name}");
                    }
                }
                else
                {
                    throw new NotFoundException(ErrorMessage.EmpNotFound);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void GetCount()
        {
            try
            {
                int? res = _empService.GetAllEmpCount();
                if (res is 0) throw new NotFoundException(ErrorMessage.EmpIsntExist);
                ConsoleColor.Cyan.WriteWithColor($"All Employees Count is {res}.");
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

    }
}


