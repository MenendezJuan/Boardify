using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Columns.Commands
{
    public class CreateColumnCommand : ICreateCommand<CreateColumnModel, CreateColumnResponseModel>
    {
        private readonly ICommandRepository<Column> _commandRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateColumnCommand(
            ICommandRepository<Column> commandRepository,
            IMapper mapper,
            IQueryRepository<Board> boardQueryRepository,
            IColumnRepository columnRepository,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _columnRepository = columnRepository ?? throw new ArgumentNullException(nameof(columnRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CreateColumnResponseModel>> Create(CreateColumnModel model)
        {
            var board = await _boardQueryRepository.GetByIdAsync(model.BoardId);
            if (board == null)
            {
                return _resultFactory.Failure<CreateColumnResponseModel>("Could not find the requested board Id.");
            }

            var columns = await _columnRepository.GetColumnsAsync(c => c.BoardId == model.BoardId);

            int position = 0;
            if (columns.Any())
            {
                int maxPosition = columns.Max(c => c.Position);
                position = maxPosition + 1;
            }

            var column = _mapper.Map<Column>(model);
            column.Position = position;

            await _commandRepository.InsertAsync(column);

            var responseModel = _mapper.Map<CreateColumnResponseModel>(column);
            return _resultFactory.Success(responseModel);
        }
    }
}