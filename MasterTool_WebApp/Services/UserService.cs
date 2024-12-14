using MasterTool_WebApp.Data;
using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using MasterTool_WebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Xml.Linq;

namespace MasterTool_WebApp.Services
{
    public class UserService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public UserService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            string query = "SELECT * FROM users";
            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<User>(query);
        }

        public async Task<List<User>> GetUserByUsernameAsync(string userName)
        {
            string query = "SELECT * FROM users WHERE user_name = @UserName";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@UserName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = userName }
            };

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<User>(query, parameters);
        }

        public async Task<List<User>> GetUserByCredentialsAsync(string userName,string password)
        {
            string query = "SELECT * FROM users WHERE user_name = @UserName AND password = @Password";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@UserName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = userName },
                new NpgsqlParameter("@Password", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = password }
            };

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<User>(query, parameters);
        }
        public async Task CreateUserAsync(string userName, string password, string phoneNumber, string name, string surname, int roleId)
        {
            string query = @"
                INSERT INTO users (user_name, password, phone_number, name, surname, role_id)
                VALUES (@UserName, @Password, @PhoneNumber, @Name, @Surname, @RoleId)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@UserName", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = userName },
                new NpgsqlParameter("@Password", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = password },
                new NpgsqlParameter("@PhoneNumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = phoneNumber },
                new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = name },
                new NpgsqlParameter("@Surname", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = surname },
                new NpgsqlParameter("@RoleId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = roleId }
            };

            await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<List<ClientWithInfoViewModel>> GetClientsWithInfoAsync()
        {
            string query = @"
                SELECT 
                    u.user_id AS UserId,
                    u.user_name AS UserName,
                    u.phone_number AS PhoneNumber,
                    u.name AS Name,
                    u.surname AS Surname,
                    COALESCE(
                        (SELECT COUNT(*)
                         FROM requests r
                         WHERE r.client_id = u.user_id), 0
                    ) AS RequestCount,
                    COALESCE(
                        (SELECT COUNT(*)
                        FROM orders o
                        INNER JOIN requests r ON o.request_id = r.request_id
                        WHERE r.client_id = u.user_id), 0
                    ) AS OrderCount
                FROM users u
                WHERE u.role_id = (
                    SELECT role_id
                    FROM roles
                    WHERE name = 'client'
                )";

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<ClientWithInfoViewModel>(query);
        }

        public async Task<List<MasterWithInfoViewModel>> GetMastersWithInfoAsync()
        {
            string query = @"
                SELECT 
                    u.user_id AS UserId,
                    u.user_name AS UserName,
                    u.phone_number AS PhoneNumber,
                    u.name AS Name,
                    u.surname AS Surname,
                    COALESCE(
                        (SELECT COUNT(*)
                        FROM orders o
                        WHERE o.master_id = u.user_id), 0
                    ) AS OrderCount
                FROM users u
                WHERE u.role_id = (
                    SELECT role_id
                    FROM roles
                    WHERE name = 'master'
                )";

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<MasterWithInfoViewModel>(query);
        }
    }
}
