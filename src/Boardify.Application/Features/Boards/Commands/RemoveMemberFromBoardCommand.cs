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
    public class RemoveMemberFromBoardCommand : IRemoveMemberFromBoardCommand
    {
        private readonly ICommandRepository<BoardMember> _boardMemberRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IBoardMemberRepository _memberRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public RemoveMemberFromBoardCommand(
            ICommandRepository<BoardMember> boardMemberRepository,
            IQueryRepository<Board> boardQueryRepository,
            IBoardMemberRepository memberRepository,
            IUserWorkspaceRepository userWorkspaceQueryRepository,
            IMapper mapper,
            IResultFactory resultFactory)
        {
            _boardMemberRepository = boardMemberRepository ?? throw new ArgumentNullException(nameof(boardMemberRepository));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _userWorkspaceQueryRepository = userWorkspaceQueryRepository ?? throw new ArgumentNullException(nameof(userWorkspaceQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<bool>> RemoveAsync(RemoveMemberFromBoardModel model, int requesterUserId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(model.BoardId);

            if (board == null)
            {
                return _resultFactory.Failure<bool>("Board not found");
            }

            var isOwner = await _userWorkspaceQueryRepository.IsOwnerOfWorkspace(requesterUserId, board.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<bool>("Only the board owner can remove members.");
            }

            var isMember = await _memberRepository.IsMemberOfBoardAsync(model.UserId, model.BoardId);
            if (!isMember)
            {
                return _resultFactory.Failure<bool>("User is not a member of the board.");
            }

            var boardMember = _mapper.Map<BoardMember>(model);

            await _boardMemberRepository.DeleteAsync(boardMember);

            return _resultFactory.Success(true);
        }
    }
}