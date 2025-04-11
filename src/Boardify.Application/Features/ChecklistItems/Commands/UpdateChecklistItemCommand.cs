using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.ChecklistItems.Commands
{
    public class UpdateChecklistItemCommand : IUpdateCommand<UpdateChecklistItemRequest, UpdateChecklistItemResponse>
    {
        private readonly ICommandRepository<ChecklistItem> _commandRepository;
        private readonly IQueryRepository<ChecklistItem> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateChecklistItemCommand(ICommandRepository<ChecklistItem> commandRepository, IMapper mapper, IQueryRepository<ChecklistItem> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateChecklistItemResponse>> Update(UpdateChecklistItemRequest request)
        {
            var checklistItem = await _queryRepository.GetByIdAsync(request.Id);
            if (checklistItem == null)
            {
                throw new KeyNotFoundException("Checklist item not found.");
            }

            _mapper.Map(request, checklistItem);
            await _commandRepository.UpdateAsync(checklistItem);
            var response = _mapper.Map<UpdateChecklistItemResponse>(checklistItem);
            return _resultFactory.Success(response);
        }
    }
}