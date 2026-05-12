using HendiMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendiMapper.ConsoleTest.Models
{
    public class EmployeeDto
    {
        public string? Name { get; set; }

        public int Age { get; set; }

        public AddressDto? Address { get; set; }

        [IgnoreMap]
        public string? Password { get; set; }
    }
}
