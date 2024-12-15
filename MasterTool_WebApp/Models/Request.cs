using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace MasterTool_WebApp.Models
{
    public class Request
    {
        [Key]
        [Column("request_id")]
        public long RequestId { get; set; } 

        [Required]
        [MaxLength(500, ErrorMessage = "Problem description cannot exceed 500 characters.")]
        [Column("problem_description")]
        public string ProblemDescription { get; set; } = string.Empty; 

        [MaxLength(50, ErrorMessage = "Manufacturer name cannot exceed 50 characters.")]
        [Column("manufacturer_name")]
        public string? ManufacturerName { get; set; }

        [Required]
        [Column("usage_time")]
        public Period UsageTime { get; set; }

        [Required]
        [ForeignKey("User")]
        [Column("client_id")]
        public long ClientId { get; set; } 

        [Required]
        [ForeignKey("Service")]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("is_order")]
        public bool IsOrder { get; set; } = false; 
    }
}
