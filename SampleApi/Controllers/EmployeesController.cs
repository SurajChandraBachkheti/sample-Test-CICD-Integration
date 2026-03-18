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
        new Employee { Id = 1, FirstName = "Alice",   LastName = "Johnson",  Department = "Engineering",    JobTitle = "Senior Developer",    Email = "alice.johnson@company.com",  JoinedDate = new DateTime(2019, 3, 10), Salary = 95000m  },
        new Employee { Id = 2, FirstName = "Bob",     LastName = "Martinez", Department = "Engineering",    JobTitle = "DevOps Engineer",      Email = "bob.martinez@company.com",   JoinedDate = new DateTime(2020, 7, 1),  Salary = 88000m  },
        new Employee { Id = 3, FirstName = "Carol",   LastName = "Smith",    Department = "HR",             JobTitle = "HR Manager",           Email = "carol.smith@company.com",    JoinedDate = new DateTime(2017, 1, 15), Salary = 72000m  },
        new Employee { Id = 4, FirstName = "David",   LastName = "Lee",      Department = "Engineering",    JobTitle = "Junior Developer",     Email = "david.lee@company.com",      JoinedDate = new DateTime(2023, 6, 20), Salary = 60000m  },
        new Employee { Id = 5, FirstName = "Emma",    LastName = "Wilson",   Department = "Marketing",      JobTitle = "Marketing Specialist", Email = "emma.wilson@company.com",    JoinedDate = new DateTime(2021, 9, 5),  Salary = 65000m  },
        new Employee { Id = 6, FirstName = "Frank",   LastName = "Brown",    Department = "Finance",        JobTitle = "Financial Analyst",    Email = "frank.brown@company.com",    JoinedDate = new DateTime(2018, 4, 12), Salary = 78000m  },
        new Employee { Id = 7, FirstName = "Grace",   LastName = "Taylor",   Department = "Marketing",      JobTitle = "Content Manager",      Email = "grace.taylor@company.com",   JoinedDate = new DateTime(2022, 2, 28), Salary = 67000m  },
        new Employee { Id = 8, FirstName = "Henry",   LastName = "Davis",    Department = "Finance",        JobTitle = "CFO",                  Email = "henry.davis@company.com",    JoinedDate = new DateTime(2015, 8, 3),  Salary = 150000m },
    };

    /// <summary>Get all employees</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(ApiResponse<List<Employee>>.Ok(_employees));
    }

    /// <summary>Get a single employee by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Employee>), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp is null)
            return NotFound(ApiResponse<Employee>.NotFound($"Employee with ID {id} was not found."));

        return Ok(ApiResponse<Employee>.Ok(emp));
    }

    /// <summary>Get employees by department</summary>
    [HttpGet("department/{department}")]
    [ProducesResponseType(typeof(ApiResponse<List<Employee>>), StatusCodes.Status200OK)]
    public IActionResult GetByDepartment(string department)
    {
        var results = _employees
            .Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(ApiResponse<List<Employee>>.Ok(results,
            results.Count > 0
                ? $"Found {results.Count} employee(s) in '{department}' department."
                : $"No employees found in '{department}' department."));
    }
}
