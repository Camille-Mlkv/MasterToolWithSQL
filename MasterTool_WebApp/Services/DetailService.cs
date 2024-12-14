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
    }
}
