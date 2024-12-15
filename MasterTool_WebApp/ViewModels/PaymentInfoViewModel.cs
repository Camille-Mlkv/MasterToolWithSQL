using System.ComponentModel.DataAnnotations.Schema;

namespace MasterTool_WebApp.ViewModels
{
    public class PaymentInfoViewModel
    {
        [Column("payment_info_id")]
        public long PaymentInfoId { get; set; }
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
    }
}
