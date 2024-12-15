using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.Models
{
    public class PaymentInfo
    {
        [Key]
        [Column("payment_info_id")] 
        public long PaymentInfoId { get; set; }

        [Required]
        [Column("creation_date")] 
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Card")]
        [Column("card_id")]
        public long CardId { get; set; }

        [Required]
        [ForeignKey("Order")]
        [Column("order_id")]
        public long OrderId { get; set; }

        public virtual CreditCard Card { get; set; }
        public virtual Order Order { get; set; }
    }
}
