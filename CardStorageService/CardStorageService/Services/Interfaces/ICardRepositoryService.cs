using CardStorageService.Data;

namespace CardStorageService.Services.Interfaces
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        IList<Card> GetByClientId(int id);
    }
}
