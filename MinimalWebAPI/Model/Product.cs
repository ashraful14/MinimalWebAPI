using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalWebAPI.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdId { get; set; }
        [Required]
        public string? ProdName { get; set; }
        public decimal Price { get; set; }
    }

   
}
