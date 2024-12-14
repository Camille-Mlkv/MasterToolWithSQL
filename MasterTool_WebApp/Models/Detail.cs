using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterTool_WebApp.Models
{
    public class Detail
    {
        [Key]
        [Column("detail_id")]
        public int DetailId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be greater than 0.")]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        [Column("amount")]
        public int Amount { get; set; }
    }

}
