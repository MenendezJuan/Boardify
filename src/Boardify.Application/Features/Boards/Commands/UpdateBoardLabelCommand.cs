using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Boards.Commands
{
    public class UpdateBoardLabelCommand : IUpdateBoardLabelCommand<UpdateBoardLabelModel, UpdateBoardLabelResponseModel>
    {
        private readonly ICommandRepository<BoardLabel> _boardLabelRepository;
        private readonly IQueryRepository<BoardLabel> _boardLabelQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateBoardLabelCommand(
            ICommandRepository<BoardLabel> boardLabelRepository,
            IMapper mapper,
            IQueryRepository<BoardLabel> boardLabelQueryRepository,
            IResultFactory resultFactory)
        {
            _boardLabelRepository = boardLabelRepository ?? throw new ArgumentNullException(nameof(boardLabelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardLabelQueryRepository = boardLabelQueryRepository ?? throw new ArgumentNullException(nameof(boardLabelQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateBoardLabelResponseModel>> Update(int boardLabelId, UpdateBoardLabelModel model, int userId)
        {
            var boardLabel = await _boardLabelQueryRepository.GetByIdAsync(boardLabelId);
            if (boardLabel == null)
            {
                return _resultFactory.Failure<UpdateBoardLabelResponseModel>("Board label not found");
            }

            _mapper.Map(model, boardLabel);
            await _boardLabelRepository.UpdateAsync(boardLabel);

            var responseModel = _mapper.Map<UpdateBoardLabelResponseModel>(boardLabel);
            return _resultFactory.Success(responseModel);
        }
    }
}