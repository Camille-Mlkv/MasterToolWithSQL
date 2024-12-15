using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class CreditCard
    {
        [Key]
        [Column("card_id")]
        public long CardId { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Card number cannot exceed 15 characters.")]
        [Column("card_number")]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(3, ErrorMessage = "CVV cannot exceed 3 characters.")]
        [Column("cvv")]
        public string CVV { get; set; } = string.Empty;

        [Required]
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("User")]
        [Column("client_id")]
        public long? ClientId { get; set; }

        // Navigation property for the related user
        public virtual User? Client { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Card type cannot exceed 20 characters.")]
        [Column("card_type")]
        public string CardType { get; set; } = string.Empty;
    }
}
