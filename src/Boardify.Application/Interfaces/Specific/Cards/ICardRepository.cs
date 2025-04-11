using Boardify.Domain.Entities;

namespace Boardify.Application.Interfaces.Specific.Cards
{
    public interface ICardRepository
    {
        Task UpdateCardsAsync(IEnumerable<Card> cards);

        Task<List<Card>> GetCardsByColumnIdAsync(int columnId);

        Task<List<Card>> GetAllCardsAsync();

        Task<Card> GetCardWithDetailsAsync(int cardId);

        Task<Card> GetCardFromColumn(int cardId);
    }
}