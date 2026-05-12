using HendiMapper.Attributes;
using HendiMapper.Exceptions;
using HendiMapper.Extensions;

namespace HendiMapper.Tests.Mapping;

public class SimpleMapperTests
{
    [Fact]
    public void Should_Map_Object_Successfully()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            Name = "Hendi",
            Age = 30
        };

        // Act
        var employee = dto.Merge<Employee>();

        // Assert
        Assert.Equal("Hendi", employee.Name);
        Assert.Equal(30, employee.Age);
    }

    [Fact]
    public void Should_Merge_Existing_Object()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            Name = "Updated",
            Age = 25
        };

        var employee = new Employee
        {
            Name = "Old",
            Age = 99
        };

        // Act
        employee.Merge(dto);

        // Assert
        Assert.Equal("Updated", employee.Name);
        Assert.Equal(25, employee.Age);
    }

    [Fact]
    public void Should_Map_Collection_Successfully()
    {
        // Arrange
        var dtos = new List<EmployeeDto>
        {
            new()
            {
                Name = "A",
                Age = 20
            },

            new()
            {
                Name = "B",
                Age = 30
            }
        };

        // Act
        var employees = dtos.Merge<EmployeeDto, Employee>();

        // Assert
        Assert.Equal(2, employees.Count);

        Assert.Equal("A", employees[0].Name);
        Assert.Equal(20, employees[0].Age);

        Assert.Equal("B", employees[1].Name);
        Assert.Equal(30, employees[1].Age);
    }

    [Fact]
    public void Should_Throw_Exception_On_Type_Mismatch()
    {
        // Arrange
        var dto = new InvalidEmployeeDto
        {
            Age = "INVALID"
        };

        // Act & Assert
        var exception = Assert.Throws<MapperException>(() =>
        {
            dto.Merge<Employee>();
        });

        Assert.Contains(
            "Property type mismatch",
            exception.Message);
    }

    [Fact]
    public void Should_Map_Nested_Object_Successfully()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            Name = "Hendi",

            Address = new AddressDto
            {
                City = "Surabaya",
                Country = "Indonesia"
            }
        };

        // Act
        var employee = dto.Merge<Employee>();

        // Assert
        Assert.NotNull(employee.Address);

        Assert.Equal(
            "Surabaya",
            employee.Address?.City);

        Assert.Equal(
            "Indonesia",
            employee.Address?.Country);
    }

    [Fact]
    public void Should_Ignore_Property_With_IgnoreMap_Attribute()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            Name = "Hendi",
            Password = "SUPER_SECRET"
        };

        // Act
        var employee = dto.Merge<Employee>();

        // Assert
        Assert.Null(employee.Password);
    }
}

public class Employee
{
    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Password { get; set; }

    public Address? Address { get; set; }
}

public class EmployeeDto
{
    public string? Name { get; set; }

    public int Age { get; set; }

    [IgnoreMap]
    public string? Password { get; set; }

    public AddressDto? Address { get; set; }
}

public class InvalidEmployeeDto
{
    public string? Age { get; set; }
}

public class Address
{
    public string? City { get; set; }

    public string? Country { get; set; }
}

public class AddressDto
{
    public string? City { get; set; }

    public string? Country { get; set; }
}