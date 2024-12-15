using MasterTool_WebApp.Models;

namespace MasterTool_WebApp.ViewModels
{
    public class CreateRequestViewModel
    {
        public string ProblemDescription { get; set; } = string.Empty;

        public string? ManufacturerName { get; set; }

        public int UsageTimeYears { get; set; }

        public int UsageTimeMonths { get; set; }

        public int ServiceId { get; set; }

        public List<Service> Services { get; set; } = new();
    }
}
