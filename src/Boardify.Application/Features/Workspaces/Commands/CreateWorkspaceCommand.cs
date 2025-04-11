using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Workspaces.Commands
{
    public class CreateWorkspaceCommand : ICreateWorkspaceCommand
    {
        private readonly ICommandRepository<Workspace> _workspaceRepository;
        private readonly ICommandRepository<UserWorkspace> _userWorkspaceRepository;
        private readonly IQueryRepository<Workspace> _workspaceQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateWorkspaceCommand(
            ICommandRepository<Workspace> workspaceRepository,
            IMapper mapper,
            IQueryRepository<Workspace> workspaceQueryRepository,
            ICommandRepository<UserWorkspace> userWorkspaceRepository,
            IResultFactory resultFactory)
        {
            _workspaceRepository = workspaceRepository ?? throw new ArgumentNullException(nameof(workspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _workspaceQueryRepository = workspaceQueryRepository ?? throw new ArgumentNullException(nameof(workspaceQueryRepository));
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CreateWorkspaceResponseModel>> Create(CreateWorkspaceModel model, int creatorUserId)
        {
            var existingWorkspace = await _workspaceQueryRepository.GetFirstOrDefaultAsync(ws => ws.Name == model.Name);

            if (existingWorkspace != null)
            {
                return _resultFactory.Failure<CreateWorkspaceResponseModel>("Ya existe un workspace con el mismo nombre.");
            }

            var workspaceEntity = _mapper.Map<Workspace>(model);

            await _workspaceRepository.InsertAsync(workspaceEntity);

            var userWorkspace = new UserWorkspace
            {
                UserId = creatorUserId,
                WorkspaceId = workspaceEntity.Id,
                IsOwner = true
            };

            await _userWorkspaceRepository.InsertAsync(userWorkspace);

            var workspaceResponse = _mapper.Map<CreateWorkspaceResponseModel>(workspaceEntity);

            return _resultFactory.Success(workspaceResponse);
        }
    }
}