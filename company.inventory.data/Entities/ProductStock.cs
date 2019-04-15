using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace company.inventory.data.Entities
{
    public class ProductStock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductStockId { get; set; }
        public int ProductId { get; set; }
        public int Available { get; set; }
        public int Reserved { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
