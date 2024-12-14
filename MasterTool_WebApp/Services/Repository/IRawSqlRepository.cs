namespace MasterTool_WebApp.Services.Repository
{
    public interface IRawSqlRepository
    {
        Task<List<T>> ExecuteRawSqlQueryAsync<T>(string sqlQuery, params object[] parameters) where T : class;
        Task<int> ExecuteNonQueryAsync(string sqlQuery, params object[] parameters);
    }
}
