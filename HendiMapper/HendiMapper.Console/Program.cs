using HendiMapper.ConsoleTest.Models;
using HendiMapper.Extensions;

Console.WriteLine("=== SINGLE OBJECT ===");

var dto = new EmployeeDto
{
    Age = 30
};

var employee = dto.Merge<Employee>();

Console.WriteLine(employee.Age);

Console.WriteLine();



Console.WriteLine("=== MERGE EXISTING OBJECT ===");

var existingEmployee = new Employee
{
    Age = 99
};

existingEmployee.Merge(dto);

Console.WriteLine(existingEmployee.Age);

Console.WriteLine();



Console.WriteLine("=== LIST OBJECT ===");

var employeeDtos = new List<EmployeeDto>
{
    new()
    {
        Age = 20
    },

    new()
    {
        Age = 25
    }
};

var employees = employeeDtos
    .Merge<EmployeeDto, Employee>();

foreach (var item in employees)
{
    Console.WriteLine(item.Age);
}

Console.WriteLine();



Console.WriteLine("=== NESTED MAPPING OBJECT ===");

var nestedDto = new EmployeeDto
{
    Name = "Hendi",
    Age = 30,
    Password = "SUPER_SECRET_PASSWORD",

    Address = new AddressDto
    {
        City = "Surabaya",
        Country = "Indonesia"
    }
};

var nestedEmployee = nestedDto.Merge<Employee>();

Console.WriteLine(nestedEmployee.Name);
Console.WriteLine(nestedEmployee.Address?.City);
Console.WriteLine(
    nestedEmployee.Password ?? "PASSWORD NOT MAPPED");

Console.WriteLine();



Console.WriteLine("=== ERROR TEST ===");

try
{
    var invalidDto = new InvalidEmployeeDto
    {
        Age = "Not a number"
    };

    invalidDto.Merge<Employee>();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}