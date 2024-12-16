using System.ComponentModel.DataAnnotations.Schema;

namespace MasterTool_WebApp.Models
{
    public class OrderDetail
    {
        [ForeignKey("Order")]
        [Column("order_id")]
        public long OrderId { get; set; }

        [ForeignKey("Detail")]
        [Column("detail_id")]
        public int DetailId { get; set; }

        [Column("used_amount")]
        public int UsedAmount { get; set; }

        // Навигационные свойства
        public virtual Order Order { get; set; }
        public virtual Detail Detail { get; set; }
    }
}
