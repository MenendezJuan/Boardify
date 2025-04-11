using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;
using Boardify.Persistence.Database;
using Boardify.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Repositories.Specifics
{
    public class UserRepository : BaseRepository<User>, IUserQueryRepository
    {
        public UserRepository(DatabaseService context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return new List<User>();

            searchText = searchText.ToLower();

            return await _entities
                  .Where(u => EF.Functions.Like(u.Name, $"%{searchText}%") ||
                            EF.Functions.Like(u.LastName, $"%{searchText}%") ||
                            EF.Functions.Like(u.Email, $"%{searchText}%") ||
                            EF.Functions.Like(u.Name + u.LastName, $"%{searchText}%"))
                .ToListAsync();
        }

        public async Task<List<User>> GetBoardMembers(int boardId)
        {
            return await _context.BoardsMember
                                 .Where(bm => bm.BoardId == boardId)
                                 .Select(bm => bm.User)
                                 .Select(u => new User
                                 {
                                     Id = u.Id,
                                     Name = u.Name,
                                     Email = u.Email,
                                     Avatar = u.Avatar,
                                     LastName = u.LastName
                                 })
                                 .ToListAsync();
        }

        public async Task<List<User>> GetWorkspaceMembers(int workspaceId)
        {
            return await _context.UserWorkspaces
                                 .Where(uw => uw.WorkspaceId == workspaceId)
                                 .Select(uw => uw.User)
                                 .Select(u => new User
                                 {
                                     Id = u.Id,
                                     Name = u.Name,
                                     Email = u.Email,
                                     Avatar = u.Avatar,
                                     LastName = u.LastName
                                 })
                                 .ToListAsync();
        }

        public async Task<List<User>> GetAssignedUsers(int cardId)
        {
            return await _context.CardMembers
                                 .Where(cm => cm.CardId == cardId)
                                 .Select(cm => cm.User)
                                 .Select(u => new User
                                 {
                                     Id = u.Id,
                                     Name = u.Name,
                                     Email = u.Email,
                                     Avatar = u.Avatar,
                                     LastName = u.LastName
                                 })
                                 .ToListAsync();
        }
    }
}