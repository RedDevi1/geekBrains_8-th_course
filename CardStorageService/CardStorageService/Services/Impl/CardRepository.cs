using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CardRepository(
            ILogger<ClientRepository> logger,
            IOptions<DatabaseOptions> databaseOptions, 
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _databaseOptions = databaseOptions;
            _context = context;
        }
        public string Create(Card data)
        {
            var client = _context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            _context.Cards.Add(data);

            _context.SaveChanges();

            return data.CardId.ToString();
        }

        public int Delete(string id)
        {
            var card = _context.Cards.FirstOrDefault(card => card.CardId.ToString() == id);
            if (card == null)
                throw new Exception("Card not found.");

            _context.Cards.Remove(card);

            _context.SaveChanges();

            return card.ClientId;
        }

        public IList<Card> GetAll()
        {
            List<Card> cards = new List<Card>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards"), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = new Guid(reader["CardId"].ToString()),
                            CardNo = reader["CardNo"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            ExpDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }
            }
            return cards;
        }

        public IList<Card> GetByClientId(int id)
        {
            List<Card> cards = new List<Card>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards where ClientId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = new Guid(reader["CardId"].ToString()),
                            CardNo = reader["CardNo"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            ExpDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }
            }
            return cards;

            //return _context.Cards.Where(card => card.ClientId == id).ToList();
        }

        public Card GetById(string id)
        {
            Card card = new Card();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards where CardId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        card.CardId = new Guid(reader["CardId"].ToString());
                        card.CardNo = reader["CardNo"]?.ToString();
                        card.Name = reader["Name"]?.ToString();
                        card.CVV2 = reader["CVV2"]?.ToString();
                        card.ExpDate = Convert.ToDateTime(reader["ExpDate"]);
                    }
                }
            }
            return card;
        }

        public int Update(Card data)
        {
            var card = _context.Cards.FirstOrDefault(card => card.CardId == data.CardId);
            if (card == null)
                throw new Exception("Card not found.");

            _context.Cards.Update(data);

            _context.SaveChanges();

            return data.ClientId;
        }
    }
}
