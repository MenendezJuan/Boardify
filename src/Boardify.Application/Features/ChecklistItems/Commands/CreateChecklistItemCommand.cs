using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Relationships;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Application.Features.ChecklistItems.Commands
{
    public class CreateChecklistItemCommand : ICreateCommand<CreateChecklistItemRequest, ChecklistItemResponse>
    {
        private readonly ICommandRepository<ChecklistItem> _commandRepository;
        private readonly IQueryRepository<ChecklistItem> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateChecklistItemCommand(ICommandRepository<ChecklistItem> commandRepository, IMapper mapper, IQueryRepository<ChecklistItem> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<ChecklistItemResponse>> Create(CreateChecklistItemRequest model)
        {
            var lastItem = await _queryRepository.FindBy(ci => ci.CardId == model.CardId)
                                                 .OrderByDescending(ci => ci.Position)
                                                 .FirstOrDefaultAsync();
            int newPosition = lastItem != null ? lastItem.Position + 1 : 1;

            var checklistItem = _mapper.Map<ChecklistItem>(model);
            checklistItem.Position = newPosition;
            checklistItem.IsChecked = false;

            await _commandRepository.InsertAsync(checklistItem);
            var response = _mapper.Map<ChecklistItemResponse>(checklistItem);
            return _resultFactory.Success(response);
        }
    }
}