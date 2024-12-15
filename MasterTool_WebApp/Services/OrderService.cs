using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MasterTool_WebApp.Services
{
    public class OrderService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public OrderService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<Order>> GetClientOrdersAsync(long clientId)
        {
            string query = @"
                            SELECT o.*
                            FROM orders o
                            INNER JOIN requests r ON o.request_id = r.request_id
                            WHERE r.client_id = @ClientId";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ClientId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = clientId }
            };
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Order>(query, parameters);
        }

        public async Task<List<OrderDetailsViewModel>> GetClientOrderDetails(int orderId)
        {
            string sql = @"
                SELECT 
                    o.order_id AS OrderId,
                    o.creation_date AS CreationDate,
                    o.is_ready AS IsReady,
                    o.request_id AS RequestId,
                    o.is_paid AS IsPaid,
                    o.total_price AS TotalPrice,
                    r.problem_description AS ProblemDescription,
                    s.name AS ServiceName,
                    s.price AS ServicePrice,
                    u.name AS MasterName
                FROM 
                    orders o
                LEFT JOIN 
                    requests r ON o.request_id = r.request_id
                LEFT JOIN 
                    services s ON r.service_id = s.service_id
                LEFT JOIN 
                    users u ON o.master_id = u.user_id
                WHERE 
                    o.order_id = @OrderId;
            "
            ;

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId }
            };

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<OrderDetailsViewModel>(sql, parameters);
        }

        public async Task PayOrder(int orderId,int cardId)
        {
            var query = @"
                UPDATE orders
                SET is_paid = true
                WHERE order_id = @OrderId;
            ";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);

            var query2 = "CALL insert_payment_info(@OrderId, @CardId);";

            var parameters2 = new[]
            {
                new NpgsqlParameter("@OrderId", orderId),
                new NpgsqlParameter("@CardId", cardId)
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query2, parameters2);
        }
    }
}
