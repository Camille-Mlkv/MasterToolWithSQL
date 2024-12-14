using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;

namespace MasterTool_WebApp.Services
{
    public class PickupPointService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public PickupPointService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<PickupPoint>> GetPointsAsync()
        {
            string query = "SELECT * FROM pickup_points";

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<PickupPoint>(query);
        }
    }
}
