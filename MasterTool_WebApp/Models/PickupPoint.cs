using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterTool_WebApp.Models
{
    public class PickupPoint
    {
        [Key]
        [Column("point_id")]
        public int PointId { get; set; } 

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Working hours cannot exceed 100 characters.")]
        [Column("working_hours")]
        public string WorkingHours { get; set; } = string.Empty;
    }
}
