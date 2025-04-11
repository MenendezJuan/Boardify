using Boardify.Domain.Relationships;

namespace Boardify.Application.Interfaces.Specific.CardLabels
{
    public interface ICardLabelRepository
    {
        Task<CardLabel?> GetByCardIdAndLabelIdAsync(int cardId, int labelId);

        Task<CardLabel?> GetByIdWithIncludes(int cardId, int labelId);

        Task<IEnumerable<CardLabel>> GetAllAsync();
    }
}