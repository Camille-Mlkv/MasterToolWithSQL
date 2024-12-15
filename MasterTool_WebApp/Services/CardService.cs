using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services.Repository;
using Npgsql;

namespace MasterTool_WebApp.Services
{
    public class CardService
    {
        private readonly IRawSqlRepository _rawSqlRepository;

        public CardService(IRawSqlRepository rawSqlRepository)
        {
            _rawSqlRepository = rawSqlRepository;
        }

        public async Task<List<CreditCard>> GetClientCardsAsync(long clientId)
        {
            string query = @"SELECT *
                            FROM credit_cards
                            WHERE client_id=@ClientId";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@ClientId", NpgsqlTypes.NpgsqlDbType.Integer) { Value = clientId }
            };

            return await _rawSqlRepository.ExecuteRawSqlQueryAsync<CreditCard>(query,parameters);
        }

        public async Task CreateCreditCardAsync(CreditCard card)
        {
            string query = @"INSERT INTO credit_cards (card_number, cvv, expiry_date, client_id,card_type)
                     VALUES (@CardNumber, @Cvv, @ExpiryDate, @ClientId,@CardType)";

            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@CardNumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = card.CardNumber },
                new NpgsqlParameter("@Cvv", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = card.CVV },
                new NpgsqlParameter("@ExpiryDate", NpgsqlTypes.NpgsqlDbType.Date) { Value = card.ExpiryDate },
                new NpgsqlParameter("@ClientId", NpgsqlTypes.NpgsqlDbType.Bigint) { Value = card.ClientId },
                new NpgsqlParameter("@CardType", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = card.CardType }
            };

             await _rawSqlRepository.ExecuteNonQueryAsync(query, parameters);
        }


    }
}
