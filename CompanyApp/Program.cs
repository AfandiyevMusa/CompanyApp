﻿

using CompanyApp.Controllers;
using DomainLayer.Entities;
using ServiceLayer.Helpers;
using ServiceLayer.Helpers.Constants;

DepartmentController departmentController = new();
EmployeeController employeeController = new();

while (true)
{
    MainMenu();

    Selection: string option = Console.ReadLine();

    int newOption;

    bool isParse = int.TryParse(option, out newOption);

    if (isParse)
    {
        switch (newOption)
        {
            case 1:
                departmentController.Create();
                break;
            case 2:
                departmentController.Update();
                break;
            case 3:
                departmentController.Delete();
                break;
            case 4:
                departmentController.GetByID();
                break;
            case 5:
                departmentController.GetAll();
                break;
            case 6:
                departmentController.Search();
                break;
            case 7:
                employeeController.Create();
                break;
            case 8:
                employeeController.Update();
                break;
            case 9:
                employeeController.GetEmpByID();
                break;
            case 10:
                employeeController.Delete();
                break;
            case 11:
                employeeController.GetAllByAge();
                break;
            case 12:
                employeeController.GetAllEmpByDepID();
                break;
            case 13:
                employeeController.GetAllEmpByDepName();
                break;
            case 14:
                employeeController.Search();
                break;
            case 15:
                employeeController.GetCount();
                break;
            default:
                break;
        }
    }
    else
    {
        ConsoleColor.DarkRed.WriteWithColor(ErrorMessage.AvailableOpt);
        goto Selection;
    }

}

static void MainMenu()
{
    ConsoleColor.Yellow.WriteWithColor("Select one of the options: ");
    Console.WriteLine(" ");
    ConsoleColor.Yellow.WriteWithColor("List the options: \n                  1 - Create Department " +
                        "                                 \n                  2 - Update Department " +
                        "                                 \n                  3 - Delete Department" +
                        "                                 \n                  4 - Get Department By ID" +
                        "                                 \n                  5 - Get All Departments " +
                        "                                 \n                  6 - Search method for departments" +
                        "                                 \n                  7 - Create Employee" +
                        "                                 \n                  8 - Update Employee" +
                        "                                 \n                  9 - Get Employee By ID" +
                        "                                 \n                  10 - Delete Employee" +
                        "                                 \n                  11 - Get Employees By Age" +
                        "                                 \n                  12 - Get Employees By Department ID" +
                        "                                 \n                  13 - Get Employees By Department Name" +
                        "                                 \n                  14 - Search Method For Employees By Name Or Surname" +
                        "                                 \n                  15 - Get All Employees Count");
}