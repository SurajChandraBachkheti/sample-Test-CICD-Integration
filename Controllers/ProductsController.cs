using Microsoft.AspNetCore.Mvc;
using SampleApi.Models;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop Pro 15",     Category = "Electronics", Price = 1299.99m, Stock = 45,  Description = "High-performance laptop with 15-inch display" },
        new Product { Id = 2, Name = "Wireless Mouse",    Category = "Accessories", Price = 29.99m,   Stock = 200, Description = "Ergonomic wireless mouse with long battery life" },
        new Product { Id = 3, Name = "Mechanical Keyboard",Category = "Accessories", Price = 89.99m,  Stock = 80,  Description = "RGB mechanical keyboard with tactile switches" },
        new Product { Id = 4, Name = "4K Monitor",        Category = "Electronics", Price = 499.99m,  Stock = 30,  Description = "27-inch 4K UHD monitor with HDR support" },
        new Product { Id = 5, Name = "USB-C Hub",         Category = "Accessories", Price = 49.99m,   Stock = 150, Description = "7-in-1 USB-C hub with HDMI and PD charging" },
        new Product { Id = 6, Name = "Noise Cancelling Headphones", Category = "Audio", Price = 199.99m, Stock = 60, Description = "Premium over-ear headphones with ANC" },
        new Product { Id = 7, Name = "Webcam HD 1080p",  Category = "Electronics", Price = 69.99m,   Stock = 95,  Description = "Full HD webcam with built-in microphone" },
        new Product { Id = 8, Name = "Desk Lamp LED",     Category = "Office",      Price = 34.99m,   Stock = 120, Description = "Adjustable LED desk lamp with USB charging port" },
    };

    /// <summary>Get all products</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_products);
    }

    /// <summary>Get a product by ID</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return product is null ? NotFound(new { message = $"Product with ID {id} not found." }) : Ok(product);
    }

    /// <summary>Get products by category</summary>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public IActionResult GetByCategory(string category)
    {
        var results = _products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(results);
    }

    /// <summary>Get all distinct categories</summary>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public IActionResult GetCategories()
    {
        var categories = _products.Select(p => p.Category).Distinct().OrderBy(c => c);
        return Ok(categories);
    }
}
