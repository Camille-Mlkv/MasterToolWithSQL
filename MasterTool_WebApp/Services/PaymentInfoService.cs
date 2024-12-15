using MasterTool_WebApp.ViewModels;
using MasterTool_WebApp.Services.Repository;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class PaymentInfoService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public PaymentInfoService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<PaymentInfoViewModel>> GetInfo()
        {
            var query = @"
                SELECT 
                    pi.payment_info_id,
                    pi.creation_date,
                    o.total_price
                FROM 
                    payment_infos pi
                JOIN 
                    orders o 
                ON 
                    pi.order_id = o.order_id;
            ";

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<PaymentInfoViewModel>(query);
        }
    }
}
