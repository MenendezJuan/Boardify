using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Users.Queries.TokenRevalidation
{
    public class TokenRevalidationQuery : IGetTokenRevalidationQuery
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;
        private readonly IQueryRepository<User> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public TokenRevalidationQuery(ITokenHandler tokenHandler, IMapper mapper, IQueryRepository<User> queryRepository, IResultFactory resultFactory)
        {
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<LoginResultModel>> RevalidateToken(string refreshToken)
        {
            try
            {
                var newAccessToken = await _tokenHandler.RefreshAccessToken(refreshToken);
                if (newAccessToken == null)
                {
                    return _resultFactory.Failure<LoginResultModel>("Failed to generate new access token.");
                }

                var accessToken = newAccessToken.AccessToken ?? string.Empty;
                var userIdString = _tokenHandler.GetUserIdFromAccessToken(accessToken);

                if (string.IsNullOrEmpty(userIdString))
                {
                    return _resultFactory.Failure<LoginResultModel>("Invalid access token, user ID not found.");
                }

                if (!int.TryParse(userIdString, out var userId))
                {
                    return _resultFactory.Failure<LoginResultModel>("Invalid user ID format in access token.");
                }

                var user = await _queryRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return _resultFactory.Failure<LoginResultModel>("User not found in the database.");
                }

                var userModel = _mapper.Map<UserModel>(user);

                var resultModel = new LoginResultModel
                {
                    Token = newAccessToken.AccessToken,
                    RefreshToken = newAccessToken.RefreshToken,
                    User = userModel
                };

                return _resultFactory.Success(resultModel);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<LoginResultModel>($"An error occurred while revalidating token: {ex.Message}");
            }
        }
    }
}