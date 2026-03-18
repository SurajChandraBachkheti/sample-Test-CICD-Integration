using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    // Static data store
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop Pro 15",       Category = "Electronics", Price = 1299.99m, Stock = 45,  Description = "High-performance laptop with 16GB RAM and 512GB SSD.", IsAvailable = true  },
        new Product { Id = 2, Name = "Wireless Mouse",      Category = "Electronics", Price = 29.99m,   Stock = 200, Description = "Ergonomic wireless mouse with long battery life.",        IsAvailable = true  },
        new Product { Id = 3, Name = "Standing Desk",       Category = "Furniture",   Price = 499.00m,  Stock = 15,  Description = "Adjustable height standing desk for home office.",       IsAvailable = true  },
        new Product { Id = 4, Name = "USB-C Hub 7-in-1",    Category = "Electronics", Price = 49.99m,   Stock = 120, Description = "7-port USB-C hub with HDMI, USB-A and SD card slots.",   IsAvailable = true  },
        new Product { Id = 5, Name = "Noise Cancel Headset",Category = "Electronics", Price = 199.00m,  Stock = 0,   Description = "Over-ear headphones with active noise cancellation.",     IsAvailable = false },
        new Product { Id = 6, Name = "Ergonomic Chair",     Category = "Furniture",   Price = 389.00m,  Stock = 8,   Description = "Lumbar-support office chair for all-day comfort.",        IsAvailable = true  },
        new Product { Id = 7, Name = "Mechanical Keyboard", Category = "Electronics", Price = 89.99m,   Stock = 75,  Description = "Tenkeyless mechanical keyboard with RGB backlight.",      IsAvailable = true  },
        new Product { Id = 8, Name = "Monitor 27\" 4K",     Category = "Electronics", Price = 649.00m,  Stock = 22,  Description = "4K UHD IPS monitor with 144Hz refresh rate.",             IsAvailable = true  },
    };

    /// <summary>Get all products</summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<Product>>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(ApiResponse<List<Product>>.Ok(_products));
    }

    /// <summary>Get a single product by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return NotFound(ApiResponse<Product>.NotFound($"Product with ID {id} was not found."));

        return Ok(ApiResponse<Product>.Ok(product));
    }

    /// <summary>Get products by category</summary>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(ApiResponse<List<Product>>), StatusCodes.Status200OK)]
    public IActionResult GetByCategory(string category)
    {
        var results = _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(ApiResponse<List<Product>>.Ok(results,
            results.Count > 0
                ? $"Found {results.Count} product(s) in category '{category}'."
                : $"No products found in category '{category}'."));
    }

    /// <summary>Get only available (in-stock) products</summary>
    [HttpGet("available")]
    [ProducesResponseType(typeof(ApiResponse<List<Product>>), StatusCodes.Status200OK)]
    public IActionResult GetAvailable()
    {
        var results = _products.Where(p => p.IsAvailable && p.Stock > 0).ToList();
        return Ok(ApiResponse<List<Product>>.Ok(results, $"{results.Count} product(s) currently in stock."));
    }
}
