using MasterTool_WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MasterTool_WebApp.ViewModels
{
    public class PayOrderViewModel
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CreditCard> ClientCards { get; set; } = new List<CreditCard>();

        [Required(ErrorMessage = "Выберите карту для оплаты.")]
        [Range(1, long.MaxValue, ErrorMessage = "А карта?")]
        public int SelectedCardId { get; set; }
    }
}
