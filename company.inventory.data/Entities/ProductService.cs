using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace company.inventory.data.Entities
{
    public class ProductService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductServiceId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
