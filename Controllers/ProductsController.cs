using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VsProductManagerWebApi.Data;
using VsProductManagerWebApi.Domain;

namespace VsProductManagerWebApi.Controllers;

// https://localhost:8000/products
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public ProductsController(ApplicationDbContext context)
    {
        this.context = context;
    }


    // GET /products
    // GET /products?productName={productName}
    /// <summary>
    /// Fetches all products
    /// </summary>
    /// <param name="productName">Filter on product name</param>
    /// <returns>Array of products</returns>
    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<ProductDto> GetProducts([FromQuery] string? productName)
    {
        IEnumerable<Product> products;

        if (string.IsNullOrEmpty(productName))
        {
            products = context.Products.ToList();
        }

        else
        {
            products = context.Products.Where(x => x.ProductName == productName);
        }

        // DTO - Data Transfer Object

           IEnumerable<ProductDto> productDtos = products.Select(x => new ProductDto
        {
            Id = x.Id,
            ProductName = x.ProductName,
            Sku = x.Sku,
            ProductDescription = x.ProductDescription,
            ImageUrl = x.ImageUrl,
            Price = x.Price

        });

        return productDtos; // 200 OK
    }



    // GET https://localhost:8000/products/{sku}
    /// <summary>
    /// Fetches product by SKU
    /// </summary>
    /// <param name="sku">Filter on product SKU</param>
    /// <returns>Single product by SKU</returns>
    [HttpGet("{sku}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDto> GetProduct(string sku)
    {
        var product = context.Products.FirstOrDefault(x => x.Sku == sku);

        if (product is null)
        { 
           return NotFound(); // 404 Not Found
        }

        var productDto = new ProductDto
        {

            Id = product.Id,
            ProductName = product.ProductName,
            Sku = product.Sku,
            ProductDescription = product.ProductDescription,
            ImageUrl = product.ImageUrl,
            Price = product.Price

        };

        return productDto; // 200 OK

    }



    // POST https://localhost:8000/products
    /// <summary>
    /// Creates a request to add product
    /// </summary>
    /// <param name="request">Add product</param>
    /// <returns>Product created</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDto> CreateProduct(CreateProductRequest request)
    {
        // Check if a product with the same SKU already exists
        if (context.Products.Any(p => p.Sku == request.SKU))
        {
            return BadRequest("A product with the same SKU already exists."); // 400 Bad Request
        }

        // Create and save the product
        var product = new Product
        {
            ProductName = request.ProductName,
            Sku = request.SKU,
            ProductDescription = request.ProductDescription,
            ImageUrl = request.ImageURL,
            Price = request.Price
        };

        context.Products.Add(product);
        context.SaveChanges();

        var productDto = new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Sku = product.Sku,
            ProductDescription = product.ProductDescription,
            ImageUrl = product.ImageUrl,
            Price = product.Price
        };

        return Created("", productDto); // 201 Created
    }



    /// <summary>
    /// Deletes product by SKU
    /// </summary>
    /// <param name="sku">Delete product by SKU</param>
    /// <returns>Product deleted</returns>
    // DELETE https://localhost:8000/products/{sku}
    [HttpDelete("{sku}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteProduct(string sku) 
    {
        var product = context.Products.FirstOrDefault(x => x.Sku == sku);

        if (product is null)
        {
            return NotFound(); // 404 Not Found
        }

        context.Products.Remove(product);

        context.SaveChanges();

        return NoContent(); // 204 No Content
    }

}

public class ProductDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProductName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Sku { get; set; }

    [Required]
    [MaxLength(250)]
    public string ProductDescription { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; }

    [Required]
    [MaxLength(15)]
    public string Price { get; set; }

}

public class CreateProductRequest
{
    [Required]
    [MaxLength(50)]
    public string ProductName { get; set; }

    [Required]
    [MaxLength(50)]
    public string SKU { get; set; }

    [Required]
    [MaxLength(250)]
    public string ProductDescription { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageURL { get; set; }

    [Required]
    [MaxLength(15)]
    public string Price { get; set; }

}