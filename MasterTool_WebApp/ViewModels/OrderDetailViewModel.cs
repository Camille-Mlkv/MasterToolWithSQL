using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.ViewModels
{
    public class OrderDetailViewModel
    {
        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("price", TypeName = "decimal(10,2)")] 
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        [Column("used_amount")] 
        public int Quantity { get; set; }
    }
}
