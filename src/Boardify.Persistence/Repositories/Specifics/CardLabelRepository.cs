using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class CardLabelRepository : BaseRepository<CardLabel>, ICardLabelRepository
    {
        public CardLabelRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<CardLabel?> GetByCardIdAndLabelIdAsync(int cardId, int labelId)
        {
            return await _entities
                .FirstOrDefaultAsync(cl => cl.CardId == cardId && cl.LabelId == labelId);
        }

        public async Task<CardLabel?> GetByIdWithIncludes(int cardId, int labelId)
        {
            return await _entities
                .Include(cl => cl.BoardLabel)
                .FirstOrDefaultAsync(cl => cl.CardId == cardId && cl.LabelId == labelId);
        }

        public async Task<IEnumerable<CardLabel>> GetAllAsync()
        {
            return await _context.CardLabels
                .Include(cl => cl.BoardLabel)
                .ToListAsync();
        }
    }
}