using System.ComponentModel.DataAnnotations;

namespace VsProductManagerWebApi.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(50)]
        public string Sku { get; set; }

        [MaxLength(250)]
        public string ProductDescription { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(15)]
        public string Price { get; set; }

    }
}
