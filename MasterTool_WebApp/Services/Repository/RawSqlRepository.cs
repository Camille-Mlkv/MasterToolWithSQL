using MasterTool_WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MasterTool_WebApp.Services.Repository
{
    public class RawSqlRepository : IRawSqlRepository
    {
        private readonly AppDbContext _context;
        public RawSqlRepository(AppDbContext context)
        {
            _context = context;
        }

        // Метод для выполнения SELECT-запросов
        public async Task<List<T>> ExecuteRawSqlQueryAsync<T>(string sqlQuery, params object[] parameters) where T : class
        {
            return await _context.Set<T>()
                                 .FromSqlRaw(sqlQuery, parameters)
                                 .ToListAsync();
        }

        // Метод для выполнения UPDATE, DELETE, INSERT запросов
        public async Task<int> ExecuteNonQueryAsync(string sqlQuery, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(sqlQuery, parameters);
        }
    }
}
