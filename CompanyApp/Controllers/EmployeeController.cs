using System;
using System.Net;
using System.Text;
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
                    ConsoleColor.Yellow.WriteWithColor("Enter employee department ID: ");
                ID: string? id = Console.ReadLine();
                    int newID;
                    bool isParseID = int.TryParse(id, out newID);

                    if (isParseID)
                    {
                        if (_depService.GetDepByID(newID) != null)
                        {
                            Department department = _depService.GetDepByID(newID);
                            ConsoleColor.Yellow.WriteWithColor("Enter employee name: ");
                        Name: string? name = Console.ReadLine().Replace(" ", "");

                            if (Regex.IsMatch(name, @"^[A-Z]{1}[a-z]+$") && name != "" && name.Length >= 3)
                            {
                                ConsoleColor.Yellow.WriteWithColor("Enter employee surname: ");
                            Surname: string? surname = Console.ReadLine().Replace(" ", "");

                                if (Regex.IsMatch(surname, @"^[A-Z]{1}[a-z]+$") && surname != "" && surname.Length >= 3)
                                {
                                    ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
                                Age: string? age = Console.ReadLine();
                                    int newAge;
                                    bool isParse = int.TryParse(age, out newAge);

                                    if (isParse && newAge >= 18)
                                    {
                                        ConsoleColor.Yellow.WriteWithColor("Enter employee address: ");
                                    Address: string? address = Console.ReadLine();

                                        if (address.Length >= 3)
                                        {
                                            var res = _empService.GetAllEmpByDepID(newID).Count;

                                            if ((res + 1) > department.Capacity)
                                            {
                                                throw new NotFoundException("Department is full!");
                                            }
                                            else
                                            {
                                                Employee employee = new()
                                                {
                                                    Name = name,
                                                    Surname = surname,
                                                    Age = newAge,
                                                    Address = address,
                                                    Department = department
                                                };

                                                var result = _empService.Create(employee);
                                                ConsoleColor.Green.WriteWithColor($"" +
                                                    $"Id: {result.Id}, " +
                                                    $"Name: {result.Name}, " +
                                                    $"Surname: {result.Surname}, " +
                                                    $"Age: {result.Age}, " +
                                                    $"Adress: {result.Address}, " +
                                                    $"DepartmentName: {result.Department?.Name}");
                                            }
                                        }
                                        else
                                        {
                                            ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectDepartment);
                                            goto Address;
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
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectID);
                        goto ID;
                    }
                }
                else
                {
                    throw new NotFoundException(ErrorMessage.DatabaseIsEmpty);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void Update() //(8)
        {
            try
            {
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter Employee ID: ");
                empID: string? empID = Console.ReadLine().Replace(" ", "");
                    int newEmpID;
                    bool isParseEmpID = int.TryParse(empID, out newEmpID);

                    if (isParseEmpID)
                    {
                        if (_depService.GetDepByID(newEmpID) != null)
                        {
                            ConsoleColor.Yellow.WriteWithColor("Enter new Department ID: ");
                        depID: string? departmentID = Console.ReadLine();
                            int newDepID;
                            bool isParseDepID = int.TryParse(departmentID, out newDepID);

                            if (isParseDepID)
                            {
                                if (_depService.GetDepByID(newDepID) != null)
                                {
                                    ConsoleColor.Yellow.WriteWithColor("Enter new Employee Name: ");
                                Name: string? name = Console.ReadLine().Replace(" ", "");

                                    if (Regex.IsMatch(name, @"^[A-Z]{1}[a-z]+$") && name != "" && name.Length >= 3)
                                    {
                                        ConsoleColor.Yellow.WriteWithColor("Enter new Employee Surname: ");
                                    Surname: string? surname = Console.ReadLine();

                                        if (Regex.IsMatch(surname, @"^[A-Z]{1}[a-z]+$") && surname != "" && surname.Length >= 3)
                                        {
                                            ConsoleColor.Yellow.WriteWithColor("Enter Employee Age: ");
                                        Age: string? empAge = Console.ReadLine();
                                            int newEmpAge;
                                            bool isParseAge = int.TryParse(empAge, out newEmpAge);

                                            ConsoleColor.Yellow.WriteWithColor("Enter new Employee Address: ");
                                        Department: string? address = Console.ReadLine();

                                            if (isParseAge && newEmpAge >= 18)
                                            {
                                                if (address.Length >= 3)
                                                {
                                                    Department department = _depService.GetDepByID(newDepID);

                                                    Employee employee = new()
                                                    {
                                                        Name = name,
                                                        Surname = surname,
                                                        Age = newEmpAge,
                                                        Address = address,
                                                        Department = department
                                                    };
                                                    if (employee is null) throw new ArgumentNullException();
                                                    var res = _empService.Update(newEmpID, employee);

                                                    if (res is null) throw new ArgumentNullException();
                                                    ConsoleColor.Cyan.WriteWithColor(ErrorMessage.Updated);
                                                }
                                                else
                                                {
                                                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.CorrectDepartment);
                                                    goto Department;
                                                }
                                            }
                                            else
                                            {
                                                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.EmpAgeShouldBeNum);
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
                                    throw new NotFoundException(ErrorMessage.DepIdNotFound);
                                }
                            }
                            else
                            {
                                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.DepIdShouldBeNum);
                                goto depID;
                            }

                            
                        }
                        else
                        {
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.EmpIdShouldBeNum);
                        goto empID;
                    }
                }
                else
                {
                    throw new NotFoundException(ErrorMessage.NoEmployee);
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
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter department ID: ");
                ID: string? depID = Console.ReadLine();
                    int newDepID;
                    bool isParse = int.TryParse(depID, out newDepID);

                    if (isParse)
                    {
                        if (_depService.GetDepByID(newDepID) != null)
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
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                        goto ID;
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
                }
                
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void Delete() //(10)
        {
            try
            {
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter employee ID: ");
                ID: string? empID = Console.ReadLine();
                    int newEmpID;
                    bool isParse = int.TryParse(empID, out newEmpID);

                    if (isParse)
                    {
                        if (_depService.GetDepByID(newEmpID) != null)
                        {
                            _empService.Delete(newEmpID);
                            ConsoleColor.Green.WriteWithColor(ErrorMessage.Deleted);
                        }
                        else
                        {
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                        goto ID;
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void GetAllByAge() //(11)
        {
            try
            {
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter employee age: ");
                Age: string? empAge = Console.ReadLine();
                    int newEmpAge;
                    bool isParse = int.TryParse(empAge, out newEmpAge);

                    if (isParse && newEmpAge >= 18)
                    {
                        var result = _empService.GetAllEmpByAge(newEmpAge);
                        if (result.Count is 0)
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
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
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
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter Employee department ID: ");
                ID: string? empID = Console.ReadLine();
                    int newEmpID;
                    bool isParse = int.TryParse(empID, out newEmpID);

                    if (isParse)
                    {
                        if (_depService.GetDepByID(newEmpID) != null)
                        {
                            var res = _empService.GetAllEmpByDepID(newEmpID);
                            if (res.Count is 0)
                            {
                                ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.DepIdNotFound);
                                goto ID;
                            }

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
                            throw new NotFoundException(ErrorMessage.DepIdNotFound);
                        }
                        
                    }
                    else
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableID);
                        goto ID;
                    }
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
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
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter Employee department Name: ");
                depName: string? depName = Console.ReadLine();

                    var res = _empService.GetAllEmpByDepName(depName);
                    if (res.Count is 0)
                    {
                        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.DepNameNotFound);
                        goto depName;
                    }

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
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void Search() //(14)
        {
            try
            {
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    ConsoleColor.Yellow.WriteWithColor("Enter employee name: ");
                Name: string? name = Console.ReadLine().Replace(" ", "");

                    if (Regex.IsMatch(name, @"^[A-Z]{1}[a-z]+$") && name != "" && name.Length >= 3)
                    {
                        ConsoleColor.Yellow.WriteWithColor("Enter employee surname: ");
                    Surname: string? surname = Console.ReadLine().Replace(" ", "");

                        if (Regex.IsMatch(surname, @"^[A-Z]{1}[a-z]+$") && surname != "" && surname.Length >= 3)
                        {
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
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

        public void GetCount() //(15)
        {
            try
            {
                if (AppDbContext<Employee>.datas?.Count != 0)
                {
                    int? res = _empService.GetAllEmpCount();
                    if (res is 0) throw new NotFoundException(ErrorMessage.EmpIsntExist);
                    ConsoleColor.Cyan.WriteWithColor($"All Employees Count is {res}.");
                }
                else
                {
                    ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.NoEmployee);
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.DarkRed.WriteWithColor(ex.Message);
            }
        }

    }
}