using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class CardMemberRepository : BaseRepository<Card>, ICardMemberRepository
    {
        public CardMemberRepository(DatabaseService context) : base(context)
        {
        }
    }
}