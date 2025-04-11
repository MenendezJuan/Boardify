using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Interfaces.Specific.Boards;

namespace Boardify.Application.Features.Boards.Queries
{
    public class GetBoardWithColumnsAndCardsQuery : IGetBoardWithColumnsAndCardsQuery
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetBoardWithColumnsAndCardsQuery(IBoardRepository boardRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<BoardResponseDto>> GetBoardColumnsAndCards(int boardId)
        {
            var board = await _boardRepository.GetByIdWithColumnsAndCardsAsync(boardId);
            if (board == null)
            {
                return _resultFactory.Failure<BoardResponseDto>("Board not found.");
            }

            var boardResponseDto = _mapper.Map<BoardResponseDto>(board);
            return _resultFactory.Success(boardResponseDto);
        }
    }
}