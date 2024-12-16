using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.ViewModels;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class DetailService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public DetailService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<Detail>> GetDetailsAsync()
        {
            string query = @"SELECT *
                            FROM details";
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Detail>(query);
        }

        public async Task CreateDetailAsync(Detail detail)
        {
            string query = @"
                INSERT INTO details (name, price, amount)
                VALUES (@Name, @Price, @Amount)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = detail.Name },
                new NpgsqlParameter("@Price", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = detail.Price },
                new NpgsqlParameter("@Amount", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detail.Amount }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }


        public async Task<Detail> FindDetailById(int id)
        {
            string query = "SELECT * FROM details WHERE detail_id=@DetailId";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            var list = await _rawSqlRepository.ExecuteRawSqlQueryAsync<Detail>(query, parameters);
            return list.First();
        }

        public async Task UpdateDetailAsync(Detail detail)
        {
            string query = @"
                UPDATE details
                SET name = @Name,
                    price = @Price,
                    amount = @Amount
                WHERE detail_id = @DetailId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detail.DetailId },
                new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = detail.Name },
                new NpgsqlParameter("@Price", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = detail.Price},
                new NpgsqlParameter("@Amount", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = detail.Amount }
              
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteDetailAsync(int id)
        {
            string query = @"
                DELETE FROM details
                WHERE detail_id = @DetailId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task AddDetailToOrder(int orderId, int detailId, int quantity)
        {
            //string query = @"INSERT INTO orders_details (order_id, detail_id, used_amount) 
            //         VALUES (@OrderId, @DetailId, @UsedAmount)";

            //var parameters = new[]
            //{
            //    new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId },
            //    new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detailId },
            //    new NpgsqlParameter("@UsedAmount", NpgsqlTypes.NpgsqlDbType.Integer) { Value = quantity }
            //};
            //await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
            // Запрос для проверки, существует ли уже такая запись
            string checkQuery = @"SELECT *
                          FROM orders_details
                          WHERE order_id = @OrderId AND detail_id = @DetailId";

            var checkParameters = new[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId },
                new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detailId }
            };

            // Выполняем запрос, чтобы проверить, существует ли запись
            var existingRecord = await _rawSqlRepository.ExecuteRawSqlQueryAsync<OrderDetail>(checkQuery, checkParameters);

            if (existingRecord.Count > 0)
            {
                string updateQuery = @"UPDATE orders_details
                               SET used_amount = used_amount + @UsedAmount
                               WHERE order_id = @OrderId AND detail_id = @DetailId";

                var updateParameters = new[]
                {
                    new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId },
                    new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detailId },
                    new NpgsqlParameter("@UsedAmount", NpgsqlTypes.NpgsqlDbType.Integer) { Value = quantity }
                };

                await _rawSqlRepository.ExecuteNonQueryAsync(updateQuery, updateParameters);
            }
            else
            {
                // Если записи нет, вставляем новую
                string insertQuery = @"INSERT INTO orders_details (order_id, detail_id, used_amount) 
                               VALUES (@OrderId, @DetailId, @UsedAmount)";

                var insertParameters = new[]
                {
                    new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId },
                    new NpgsqlParameter("@DetailId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = detailId },
                    new NpgsqlParameter("@UsedAmount", NpgsqlTypes.NpgsqlDbType.Integer) { Value = quantity }
                };  

                await _rawSqlRepository.ExecuteNonQueryAsync(insertQuery, insertParameters);
            }
        }


        public async Task<List<OrderDetailViewModel>> GetDetailsForOrder(int orderId)
        {
            string query = @"SELECT d.name, od.used_amount, d.price
                     FROM orders_details od
                     JOIN details d ON od.detail_id = d.detail_id
                     WHERE od.order_id = @OrderId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@OrderId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = orderId }
            };

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<OrderDetailViewModel>(query, parameters);
        }
    }
}
