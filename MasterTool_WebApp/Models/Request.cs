using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class Request
    {
        [Key]
        public long RequestId { get; set; } 

        [Required]
        [MaxLength(500, ErrorMessage = "Problem description cannot exceed 500 characters.")]
        public string ProblemDescription { get; set; } = string.Empty; 

        [MaxLength(50, ErrorMessage = "Manufacturer name cannot exceed 50 characters.")]
        public string? ManufacturerName { get; set; }

        [Required]
        public string UsageTime { get; set; } = string.Empty;

        [Required]
        [ForeignKey("User")]
        public long ClientId { get; set; } 

        [Required]
        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        public bool IsOrder { get; set; } = false; 
    }
}
