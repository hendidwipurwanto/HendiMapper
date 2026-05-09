using HendiMapper.ConsoleTest.Models;
using HendiMapper.Extensions;

var dto = new EmployeeDto
{
    Name = "Hendi",
    Age = 30
};

Console.WriteLine("=== SINGLE OBJECT ===");

var employee = dto.Merge<Employee>();

Console.WriteLine(employee.Name);
Console.WriteLine(employee.Age);

Console.WriteLine();

Console.WriteLine("=== MERGE EXISTING OBJECT ===");

var existingEmployee = new Employee
{
    Name = "Old Name",
    Age = 99
};

existingEmployee.Merge(dto);

Console.WriteLine(existingEmployee.Name);
Console.WriteLine(existingEmployee.Age);

Console.WriteLine();

Console.WriteLine("=== LIST OBJECT ===");

var employeeDtos = new List<EmployeeDto>
{
    new()
    {
        Name = "A",
        Age = 20
    },

    new()
    {
        Name = "B",
        Age = 25
    }
};

var employees = employeeDtos.Merge<EmployeeDto, Employee>();

foreach (var item in employees)
{
    Console.WriteLine($"{item.Name} - {item.Age}");
}