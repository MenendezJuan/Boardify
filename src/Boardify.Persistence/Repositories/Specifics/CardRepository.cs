using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(DatabaseService context) : base(context)
        {
        }

        public async Task UpdateCardsAsync(IEnumerable<Card> cards)
        {
            _entities.UpdateRange(cards);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Card>> GetCardsByColumnIdAsync(int columnId)
        {
            return await _context.Cards
                                 .Where(card => card.ColumnId == columnId)
                                 .OrderBy(card => card.Id) 
                                 .ToListAsync();
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                                 .OrderBy(card => card.Id) 
                                 .ToListAsync();
        }
        public async Task<Card> GetCardWithDetailsAsync(int cardId)
        {
            var card = await _context.Cards
                .Include(c => c.CardLabels).ThenInclude(l => l.BoardLabel)
                .Include(c => c.CardAttachments)
                .Include(c => c.CardMembers).ThenInclude(cm => cm.User)
                .Include(c => c.Reporter)
                .Include(c => c.ChecklistItem)
                .Include(c => c.CardComments).ThenInclude(cc => cc.User)
                .FirstOrDefaultAsync(c => c.Id == cardId);

            if (card == null)
            {
                throw new NotFoundException("Card not found.");
            }

            return card;
        }

        public async Task<Card> GetCardFromColumn(int cardId)
        {
            var card = await _context.Cards
                .Include(c => c.Column)
                    .ThenInclude(col => col.Board)
                .FirstOrDefaultAsync(c => c.Id == cardId);

            if (card == null)
            {
                throw new NotFoundException("Card not found.");
            }

            return card;
        }
    }
}