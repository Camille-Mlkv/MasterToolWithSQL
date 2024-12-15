using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

        public async Task<List<Feedback>> FindFeedbackById(int orderId)
        {
            string query = @"SELECT *
                            FROM  feedbacks f
                            WHERE order_id=@OrderId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId }
            };
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Feedback>(query,parameters);
        }

        public async Task CreateFeedback(Feedback model)
        {
            var query = @"
                INSERT INTO Feedbacks (order_id, text, rating)
                VALUES (@OrderId, @Text, @Rating);
            ";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = model.OrderId },
                new NpgsqlParameter("@Text", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = model.Text },
                new NpgsqlParameter("@Rating", NpgsqlTypes.NpgsqlDbType.Integer) { Value = model.Rating },
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query,parameters);
        }
    }
}
