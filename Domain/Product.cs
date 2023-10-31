using System.ComponentModel.DataAnnotations;

namespace VsProductManagerWebApi.Domain;

//[Index(nameof(Sku), IsUnique = true)]
public class Product
{
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

};
