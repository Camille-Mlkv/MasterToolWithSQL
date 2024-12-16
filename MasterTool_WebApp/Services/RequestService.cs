using MasterTool_WebApp.LocalData;
using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class RequestService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public RequestService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<Request>> GetClientRequestsAsync(long clientId)
        {
            string query = @"SELECT *
                            FROM requests
                            WHERE client_id=@ClientId";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ClientId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = clientId }
            };
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Request>(query,parameters);
        }

        public async Task CreateRequestAsync(Request request)
        {
            string query = @"
                        INSERT INTO requests (problem_description, manufacturer_name, usage_time, client_id, service_id, is_order)
                        VALUES (@ProblemDescription, @ManufacturerName, @UsageTime, @ClientId, @ServiceId, @IsOrder)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ProblemDescription", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = request.ProblemDescription },
                new NpgsqlParameter("@ManufacturerName", NpgsqlTypes.NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrEmpty(request.ManufacturerName) ? DBNull.Value : request.ManufacturerName
                },
                new NpgsqlParameter("@UsageTime", NpgsqlTypes.NpgsqlDbType.Interval) { Value = request.UsageTime },
                new NpgsqlParameter("@ClientId", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = request.ClientId },
                new NpgsqlParameter("@ServiceId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = request.ServiceId },
                new NpgsqlParameter("@IsOrder", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = request.IsOrder }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }


        public async Task<List<Request>> GetOpenRequestsAsync()
        {
            string query = @"SELECT *
                            FROM requests
                            WHERE is_order=false";
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Request>(query);
        }

        public async Task RequestToOrderAsync(int requestId)
        {
            // create an order
            var isPaid = false;
            var totalPrice = 0;

            string query = @"
                INSERT INTO orders (master_id, request_id, is_paid, total_price)
                VALUES (@MasterId, @RequestId, @IsPaid, @TotalPrice)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@MasterId", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = CurrentUser.UserId },
                new NpgsqlParameter("@RequestId", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = requestId },
                new NpgsqlParameter("@IsPaid", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = isPaid },
                new NpgsqlParameter("@TotalPrice", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = totalPrice }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

    }
}
