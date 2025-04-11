using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class CreateCardCommand : ICreateCommand<CreateCardModel, CreateCardResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateCardCommand(
            ICommandRepository<Card> commandRepository,
            IMapper mapper,
            IColumnRepository columnRepository,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _columnRepository = columnRepository ?? throw new ArgumentNullException(nameof(columnRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CreateCardResponseModel>> Create(CreateCardModel request)
        {
            try
            {
                var column = await _columnRepository.GetByIdWithCardsAsync(request.ColumnId);
                if (column == null)
                {
                    return _resultFactory.Failure<CreateCardResponseModel>("Column not found.");
                }

                var card = _mapper.Map<Card>(request);

                await _commandRepository.InsertAsync(card);

                var response = _mapper.Map<CreateCardResponseModel>(card);

                return _resultFactory.Success(response);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<CreateCardResponseModel>($"An error occurred while creating the card: {ex.Message}");
            }
        }
    }
}