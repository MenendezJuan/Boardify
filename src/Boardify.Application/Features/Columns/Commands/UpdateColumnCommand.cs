using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Columns.Commands
{
    public class UpdateColumnCommand : IUpdateCommand<UpdateColumnModel, UpdateColumnResponseModel>
    {
        private readonly ICommandRepository<Column> _commandRepository;
        private readonly IQueryRepository<Column> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateColumnCommand(
            ICommandRepository<Column> commandRepository,
            IMapper mapper,
            IQueryRepository<Column> queryRepository,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateColumnResponseModel>> Update(UpdateColumnModel model)
        {
            var column = await _queryRepository.GetByIdAsync(model.Id);

            if (column == null)
            {
                return _resultFactory.Failure<UpdateColumnResponseModel>("Column not found.");
            }

            _mapper.Map(model, column);

            await _commandRepository.UpdateAsync(column);

            var response = _mapper.Map<UpdateColumnResponseModel>(column);
            return _resultFactory.Success(response);
        }
    }
}