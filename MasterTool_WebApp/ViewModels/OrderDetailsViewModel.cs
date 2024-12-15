namespace MasterTool_WebApp.ViewModels
{
    public class OrderDetailsViewModel
    {
        // Поля заказа
        public long OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsReady { get; set; }
        public long RequestId { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalPrice { get; set; }

        // Поля заявки (Request)
        public string ProblemDescription { get; set; }

        // Поля услуги (Service)
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }

        // Имя мастера (User)
        public string MasterName { get; set; }
    }
}
