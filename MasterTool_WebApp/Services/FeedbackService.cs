using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.ViewModels;

namespace MasterTool_WebApp.Services
{
    public class FeedbackService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public FeedbackService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }
        public async Task<List<FeedbackWithClientNameViewModel>> GetFeedbacksInfoAsync()
        {
            string query = @"SELECT 
                                f.feedback_id AS FeedbackId,
                                f.text AS Text,
                                f.rating AS Rating,
                                f.order_id AS OrderId,
                                u.name AS ClientName
                            FROM 
                                feedbacks f
                            JOIN 
                                orders o ON f.order_id = o.order_id
                            JOIN 
                                requests r ON o.request_id = r.request_id
                            JOIN 
                                users u ON r.client_id = u.user_id;
                            ";
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<FeedbackWithClientNameViewModel>(query);
        }
    }
}
