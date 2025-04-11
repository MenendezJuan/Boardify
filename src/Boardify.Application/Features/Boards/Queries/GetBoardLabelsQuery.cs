using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Specific.Boards;

namespace Boardify.Application.Features.Boards.Queries
{
    public class GetBoardLabelsQuery : IGetBoardLabelsQuery
    {
        private readonly IMapper _mapper;
        private readonly IBoardLabelRepository _boardLabelRepository;
        private readonly IResultFactory _resultFactory;

        public GetBoardLabelsQuery(IMapper mapper, IBoardLabelRepository boardLabelRepository, IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardLabelRepository = boardLabelRepository ?? throw new ArgumentNullException(nameof(boardLabelRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<BoardLabelModel>>> GetBoardLabelsAsync(int boardId)
        {
            var boardLabels = await _boardLabelRepository.GetBoardLabels(boardId);
            var boardLabelModels = _mapper.Map<IEnumerable<BoardLabelModel>>(boardLabels);

            return _resultFactory.Success(boardLabelModels);
        }
    }
}