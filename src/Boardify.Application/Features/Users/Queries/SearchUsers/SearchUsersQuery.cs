using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces.Specific.Users;

namespace Boardify.Application.Features.Users.Queries.SearchUsers
{
    public class SearchUsersQuery : ISearchUsersQuery
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public SearchUsersQuery(IUserQueryRepository userQueryRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<SearchUserModel>>> SearchUsersAsync(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return _resultFactory.Failure<IEnumerable<SearchUserModel>>("Search text cannot be empty.");
                }

                var users = await _userQueryRepository.SearchUsersAsync(searchText);

                if (users == null || !users.Any())
                {
                    return _resultFactory.Failure<IEnumerable<SearchUserModel>>("No users found matching the search criteria.");
                }

                var userModels = _mapper.Map<IEnumerable<SearchUserModel>>(users);
                return _resultFactory.Success(userModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<SearchUserModel>>($"An error occurred while searching for users: {ex.Message}");
            }
        }
    }
}