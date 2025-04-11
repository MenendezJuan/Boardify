using Boardify.Application.DTOs;
using Boardify.Domain.Entities;

namespace Boardify.Application.Interfaces
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(User user);

        string CreateRefreshToken(User user);

        string GetUserIdFromAccessToken(string accessToken);

        Task<TokenDto> RefreshAccessToken(string refreshToken);
    }
}