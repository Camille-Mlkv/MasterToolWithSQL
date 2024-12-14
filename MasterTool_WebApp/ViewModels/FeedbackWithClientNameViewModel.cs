using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.ViewModels
{
    public class FeedbackWithClientNameViewModel
    {
        [Key]
        public long FeedbackId { get; set; } 

        [Required]
        [MaxLength(500, ErrorMessage = "Feedback text cannot exceed 500 characters.")]
        public string Text { get; set; } = string.Empty; 

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required]
        [ForeignKey("Order")]
        public long OrderId { get; set; } 

        public string ClientName { get; set; } = string.Empty; 
    }
}
