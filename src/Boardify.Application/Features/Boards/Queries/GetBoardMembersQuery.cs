using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Specific.Boards;

namespace Boardify.Application.Features.Boards.Queries
{
    public class GetBoardMembersQuery : IGetBoardMembersQuery
    {
        private readonly IMapper _mapper;
        private readonly IBoardMemberRepository _boardMemberRepository;
        private readonly IResultFactory _resultFactory;

        public GetBoardMembersQuery(IMapper mapper, IBoardMemberRepository boardMemberRepository, IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardMemberRepository = boardMemberRepository ?? throw new ArgumentNullException(nameof(boardMemberRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<BoardMemberModel>>> GetBoardMembersAsync(int boardId)
        {
            var boardMembers = await _boardMemberRepository.GetBoardMembers(boardId);

            if (boardMembers == null || !boardMembers.Any())
            {
                return _resultFactory.Failure<IEnumerable<BoardMemberModel>>("No members found for the specified board.");
            }

            var boardMemberModels = _mapper.Map<IEnumerable<BoardMemberModel>>(boardMembers);
            return _resultFactory.Success(boardMemberModels);
        }
    }
}