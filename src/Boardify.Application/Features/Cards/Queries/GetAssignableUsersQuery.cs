using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Cards.Queries
{
    public class GetAssignableUsersQuery : IGetAssignableUsersQuery
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserQueryRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;
        public GetAssignableUsersQuery(ICardRepository cardRepository, IUserQueryRepository userRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<List<UserResponseModel>>> GetAssignableUsers(int cardId)
        {
            try
            {
                var card = await _cardRepository.GetCardFromColumn(cardId);
                if (card == null)
                    return _resultFactory.Failure<List<UserResponseModel>>("Card not found.");

                if (card.Column == null || card.Column.Board == null)
                    return _resultFactory.Failure<List<UserResponseModel>>("Column or Board data is missing.");

                var board = card.Column.Board;

                List<User> users = new List<User>();
                if (board.Visibility == VisibilityEnum.Private || board.Visibility == VisibilityEnum.Public)
                {
                    users = await _userRepository.GetBoardMembers(board.Id);
                }
                else if (board.Visibility == VisibilityEnum.Workspace)
                {
                    users = await _userRepository.GetBoardMembers(board.Id);
                    var workspaceUsers = await _userRepository.GetWorkspaceMembers(board.WorkspaceId);
                    users.AddRange(workspaceUsers);
                }

                var assignedUsers = await _userRepository.GetAssignedUsers(cardId);
                users = users.Concat(assignedUsers).GroupBy(u => u.Id).Select(g => g.First()).ToList();

                var userModels = _mapper.Map<List<UserResponseModel>>(users);
                return _resultFactory.Success(userModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<List<UserResponseModel>>(ex.Message);
            }
        }
    }
}