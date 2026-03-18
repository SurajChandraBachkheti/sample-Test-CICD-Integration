using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private static readonly List<Employee> _employees = new()
    {
        new Employee { Id = 1, FirstName = "Alice",   LastName = "Johnson",  Department = "Engineering",  Email = "alice.johnson@company.com",  Position = "Senior Developer",    Salary = 95000m  },
        new Employee { Id = 2, FirstName = "Bob",     LastName = "Smith",    Department = "Engineering",  Email = "bob.smith@company.com",      Position = "Junior Developer",    Salary = 65000m  },
        new Employee { Id = 3, FirstName = "Carol",   LastName = "Williams", Department = "HR",           Email = "carol.williams@company.com", Position = "HR Manager",          Salary = 72000m  },
        new Employee { Id = 4, FirstName = "David",   LastName = "Brown",    Department = "Marketing",    Email = "david.brown@company.com",    Position = "Marketing Lead",      Salary = 78000m  },
        new Employee { Id = 5, FirstName = "Emma",    LastName = "Davis",    Department = "Finance",      Email = "emma.davis@company.com",     Position = "Financial Analyst",   Salary = 82000m  },
        new Employee { Id = 6, FirstName = "Frank",   LastName = "Miller",   Department = "Engineering",  Email = "frank.miller@company.com",   Position = "DevOps Engineer",     Salary = 90000m  },
        new Employee { Id = 7, FirstName = "Grace",   LastName = "Wilson",   Department = "Design",       Email = "grace.wilson@company.com",   Position = "UI/UX Designer",      Salary = 75000m  },
        new Employee { Id = 8, FirstName = "Henry",   LastName = "Moore",    Department = "Marketing",    Email = "henry.moore@company.com",    Position = "Content Strategist",  Salary = 68000m  },
    };

    /// <summary>Get all employees</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_employees);
    }

    /// <summary>Get an employee by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        return employee is null ? NotFound(new { message = $"Employee with ID {id} not found." }) : Ok(employee);
    }

    /// <summary>Get employees by department</summary>
    [HttpGet("department/{department}")]
    [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
    public IActionResult GetByDepartment(string department)
    {
        var results = _employees
            .Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(results);
    }

    /// <summary>Get all departments</summary>
    [HttpGet("departments")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public IActionResult GetDepartments()
    {
        var departments = _employees.Select(e => e.Department).Distinct().OrderBy(d => d);
        return Ok(departments);
    }
}
