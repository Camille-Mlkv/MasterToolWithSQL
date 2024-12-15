using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Order
    {
        [Key]
        [Column("order_id")]
        public long OrderId { get; set; }

        [Required]
        [Column("creation_date")]
        public DateTime CreationDate { get; set; } = DateTime.Now; 

        [Required]
        [ForeignKey("User")]
        [Column("master_id")]
        public long MasterId { get; set; } 

        [Required]
        [Column("is_ready")]
        public bool IsReady { get; set; } = false; 

        [Required]
        [ForeignKey("Request")]
        [Column("request_id")]
        public long RequestId { get; set; }

        [Column("is_paid")]
        public bool IsPaid { get; set; } = false;

        [Required]
        [Column("total_price", TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
        public decimal TotalPrice { get; set; } = 0;
    }
}
