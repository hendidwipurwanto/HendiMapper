# HendiMapper

A lightweight zero-configuration object mapper for .NET focused on simplicity, predictability, and developer experience.

HendiMapper is built for developers who want a simple, predictable, and minimal mapping experience without heavy configuration or magical behavior.

---

# Features

* Zero configuration
* Extension method based API
* Strict type validation
* Detailed custom exception handling
* Object to object mapping
* Collection mapping support
* Reflection property caching
* Lightweight and easy to use

---

# Why HendiMapper?

Most object mappers become overly complex over time.

HendiMapper focuses on:

* Predictable behavior
* Developer-friendly debugging
* Minimal setup
* Explicit validation
* Clean API design

Example:

```csharp
employee.Merge(employeeDto);
```

Instead of:

```csharp
_mapper.Map(employeeDto, employee);
```

---

# Installation

## NuGet

```bash
dotnet add package HendiMapper
```

---

# Quick Start

## Models

```csharp
public class Employee
{
    public string Name { get; set; }

    public int Age { get; set; }
}

public class EmployeeDto
{
    public string Name { get; set; }

    public int Age { get; set; }
}
```

---

# Single Object Mapping

```csharp
using HendiMapper.Extensions;

var dto = new EmployeeDto
{
    Name = "Hendi",
    Age = 30
};

var employee = dto.Merge<Employee>();
```

---

# Merge Existing Object

```csharp
employee.Merge(dto);
```

---

# Collection Mapping

```csharp
var employees = employeeDtos.Merge<EmployeeDto, Employee>();
```

---

# Strict Type Validation

HendiMapper intentionally uses strict type validation to avoid hidden conversions and unpredictable behavior.

Example:

```csharp
public class EmployeeDto
{
    public string Age { get; set; }
}

public class Employee
{
    public int Age { get; set; }
}
```

Result:

```text
Property type mismatch for 'Age'.
Source type: 'String', Destination type: 'Int32'.
```

---

# Error Handling

HendiMapper provides detailed exception messages through custom exceptions.

Example:

```text
Failed to map property 'Age'.
```

---

# Performance

HendiMapper includes reflection property caching using:

* ConcurrentDictionary
* Cached PropertyInfo lookup
* Reusable reflection metadata

This significantly reduces repeated reflection overhead during mapping operations.

---
# Benchmark

Benchmark executed using:

- .NET 8
- BenchmarkDotNet
- Intel Core i7-1355U
- Windows 11

## Benchmark Result

| Mapper        | Mean       | Allocated |
|----------------|------------|------------|
| ManualMapping | 4.142 ns   | 32 B       |
| HendiMapper   | 125.134 ns | 296 B      |
| AutoMapper    | 49.313 ns  | 32 B       |

## Analysis

Manual mapping is naturally the fastest because it uses direct property assignment without reflection or abstraction.

HendiMapper currently uses:

- Reflection-based mapping
- Property metadata caching
- Strict runtime validation

AutoMapper achieves better performance through advanced optimizations such as:

- Compiled expression trees
- Delegate caching
- Internal runtime optimizations

HendiMapper prioritizes:

- Simplicity
- Predictability
- Minimal setup
- Developer-friendly debugging

while maintaining reasonable performance for most application scenarios.

# Current Architecture

```text
HendiMapper
│
├── Core
│   ├── SimpleMapper.cs
│   └── PropertyCache.cs
│
├── Extensions
│   └── MergeExtensions.cs
│
└── Exceptions
    └── MapperException.cs
```

---

# Roadmap

## Completed

* Object mapping
* Collection mapping
* Strict validation
* Custom exceptions
* Reflection caching

## Planned

* Nullable support
* Enum support
* Nested object mapping
* Expression tree optimization
* Source generator support
* Expression tree optimization
* Dictionary-based property lookup optimization

---

# Philosophy

HendiMapper is intentionally:

* Minimal
* Explicit
* Predictable
* Easy to debug
* Easy to learn

The goal is NOT to replace every mapper.

The goal is to provide a clean alternative for developers who prefer simplicity.

---

# Author

Created by Hendi Dwi Purwanto.

ASP.NET developer focused on building scalable web systems and developer tools.
