using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Boards.Commands
{
    public class AddMemberToBoardCommand : IAddMemberToBoardCommand
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepository<BoardMember> _boardMemberRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IBoardMemberRepository _memberRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceQueryRepository;
        private readonly IResultFactory _resultFactory;

        public AddMemberToBoardCommand(
            IMapper mapper,
            ICommandRepository<BoardMember> boardMemberRepository,
            IBoardMemberRepository memberRepository,
            IQueryRepository<Board> boardQueryRepository,
            IUserWorkspaceRepository userWorkspaceQueryRepository,
            IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardMemberRepository = boardMemberRepository ?? throw new ArgumentNullException(nameof(boardMemberRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _userWorkspaceQueryRepository = userWorkspaceQueryRepository ?? throw new ArgumentNullException(nameof(userWorkspaceQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<AddMemberToBoardResponseModel>> Create(AddMemberToBoardModel model, int creatorUserId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(model.BoardId);

            if (board == null)
            {
                return _resultFactory.Failure<AddMemberToBoardResponseModel>("Board not found");
            }

            var isOwner = await _userWorkspaceQueryRepository.IsOwnerOfWorkspace(creatorUserId, board.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<AddMemberToBoardResponseModel>("Only the board owner can add members.");
            }

            var isMember = await _memberRepository.IsMemberOfBoardAsync(model.UserId, model.BoardId);
            if (isMember)
            {
                return _resultFactory.Failure<AddMemberToBoardResponseModel>("User is already a member of the board.");
            }

            var boardMember = _mapper.Map<BoardMember>(model);
            await _boardMemberRepository.InsertAsync(boardMember);

            var responseModel = _mapper.Map<AddMemberToBoardResponseModel>(boardMember);
            return _resultFactory.Success(responseModel);
        }
    }
}