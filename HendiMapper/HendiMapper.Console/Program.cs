using HendiMapper.ConsoleTest.Models;
using HendiMapper.Extensions;

var dto = new EmployeeDto
{
   
    Age = "30"
};

Console.WriteLine("=== SINGLE OBJECT ===");

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
        Age = "20"
    },

    new()
    {
        Age = "25"
    }
};

var employees = employeeDtos.Merge<EmployeeDto, Employee>();

foreach (var item in employees)
{
    Console.WriteLine(item.Age);
}
try
{
    var invalidDto = new InvalidEmployeeDto
    {
       Age = "Not a number"
    };

    var employeeInvalid = invalidDto.Merge<Employee>();
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("=== ERROR TEST ===");
    Console.WriteLine(ex.Message);
}