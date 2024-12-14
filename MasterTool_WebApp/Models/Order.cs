using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Order
    {
        [Key]
        public long OrderId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now; 

        [Required]
        [ForeignKey("User")]
        public long MasterId { get; set; } 

        [Required]
        public bool IsReady { get; set; } = false; 

        [Required]
        public bool IsTaken { get; set; } = false; 

        [ForeignKey("PickupPoint")]
        public int? PointId { get; set; } 

        [Required]
        [ForeignKey("Request")]
        public long RequestId { get; set; } 

        public bool IsPaid { get; set; } = false;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
        public decimal TotalPrice { get; set; } = 0;
    }
}
