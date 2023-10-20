using Microsoft.AspNetCore.Mvc;
using VsProductManagerWebApi.Data;
using VsProductManagerWebApi.Domain;

namespace VsProductManagerWebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public ProductsController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        var products = context.Products.ToList();

        return products;
    }
}
