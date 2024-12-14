using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.Models;
using MasterTool_WebApp.ViewModels;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class ServicesService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public ServicesService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<Service>> GetServicesAsync()
        {
            string query = "SELECT * FROM services";

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Service>(query);
        }

        public async Task CreateServiceAsync(Service service)
        {
            string query = @"
                INSERT INTO services (name, description, price, is_available)
                VALUES (@Name, @Description, @Price, @IsAvailable)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = service.Name },
                new NpgsqlParameter("@Description", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = service.Description ?? (object)DBNull.Value },
                new NpgsqlParameter("@Price", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = service.Price },
                new NpgsqlParameter("@IsAvailable", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = service.IsAvailable }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<Service> FindServiceById(int id)
        {
            string query = "SELECT * FROM services WHERE service_id=@ServiceId";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ServiceId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            var list= await _rawSqlRepository.ExecuteRawSqlQueryAsync<Service>(query,parameters);
            return list.First();
        }

        public async Task UpdateServiceAsync(Service service)
        {
            string query = @"
                UPDATE services
                SET name = @Name,
                    description = @Description,
                    price = @Price,
                    is_available = @IsAvailable
                WHERE service_id = @ServiceId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ServiceId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = service.ServiceId },
                new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = service.Name },
                new NpgsqlParameter("@Description", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = service.Description ?? (object)DBNull.Value },
                new NpgsqlParameter("@Price", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = service.Price },
                new NpgsqlParameter("@IsAvailable", NpgsqlTypes.NpgsqlDbType.Boolean) { Value = service.IsAvailable }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteServiceAsync(int id)
        {
            string query = @"
                DELETE FROM services
                WHERE service_id = @ServiceId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ServiceId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = id }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

    }
}
