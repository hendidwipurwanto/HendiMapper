using AutoMapper;
using BenchmarkDotNet.Attributes;
using HendiMapper.Benchmark.Models;
using HendiMapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HendiMapper.Benchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class MapperBenchmark
    {
        private EmployeeDto _dto;

        private IMapper _autoMapper;

        [GlobalSetup]
        public void Setup()
        {
            _dto = new EmployeeDto
            {
                Name = "Hendi",
                Age = 30
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDto, Employee>();
            });

            _autoMapper = config.CreateMapper();
        }

        [Benchmark]
        public Employee ManualMapping()
        {
            return new Employee
            {
                Name = _dto.Name,
                Age = _dto.Age
            };
        }

        [Benchmark]
        public Employee HendiMapper()
        {
            return _dto.Merge<Employee>();
        }

        [Benchmark]
        public Employee AutoMapper()
        {
            return _autoMapper.Map<Employee>(_dto);
        }
    }
}
