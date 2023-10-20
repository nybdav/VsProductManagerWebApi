using Microsoft.EntityFrameworkCore;
using VsProductManagerWebApi.Domain;

namespace VsProductManagerWebApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
        options) 
        : base(options) 
    { }

    public DbSet<Product> Products { get; set; }
}


