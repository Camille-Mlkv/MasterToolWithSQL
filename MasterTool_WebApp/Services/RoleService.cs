using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class RoleService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public RoleService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            string query = "SELECT * FROM roles";
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Role>(query);
        }

        public async Task<List<Role>> GetRoleByIdAsync(int? roleId)
        {
            string query = "SELECT * FROM roles WHERE role_id = @RoleId";

            // Параметры для SQL-запроса
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@RoleId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = roleId }
            };

            // Выполнение запроса и возврат результата
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<Role>(query, parameters);
        }
    }
}
