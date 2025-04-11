using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Specific.Columns;

namespace Boardify.Application.Features.Columns.Commands
{
    public class UpdateColumnOrderCommand : IUpdateColumnOrderCommand
    {
        private readonly IColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateColumnOrderCommand(IColumnRepository columnRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _columnRepository = columnRepository ?? throw new ArgumentNullException(nameof(columnRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<List<ColumnResponseModel>>> UpdateColumnOrder(UpdateColumnOrderModel model)
        {
            var columns = await _columnRepository.GetColumnsByBoardIdAsync(model.BoardId);
            if (columns == null || !columns.Any())
            {
                return _resultFactory.Failure<List<ColumnResponseModel>>("No columns found for the specified board.");
            }

            var newPositionMap = model.ColumnOrder.Select((id, index) => new { Id = id, Position = index + 1 })
                                                   .ToDictionary(item => item.Id, item => item.Position);

            foreach (var column in columns)
            {
                if (newPositionMap.TryGetValue(column.Id, out var newPosition))
                {
                    column.Position = newPosition;
                }
            }

            await _columnRepository.UpdateColumnsAsync(columns);

            var orderedColumns = columns.OrderBy(c => c.Position).ToList();
            var columnResponseModels = _mapper.Map<List<ColumnResponseModel>>(orderedColumns);

            return _resultFactory.Success(columnResponseModels);
        }
    }
}