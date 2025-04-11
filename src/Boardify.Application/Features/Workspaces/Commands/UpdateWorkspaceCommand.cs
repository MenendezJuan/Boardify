using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Workspaces.Commands
{
    public class UpdateWorkspaceCommand : IUpdateWorkspaceCommand<UpdateWorkspaceModel, UpdateWorkspaceResponseModel>
    {
        private readonly ICommandRepository<Workspace> _workspaceRepository;
        private readonly IQueryRepository<Workspace> _workspaceQueryRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateWorkspaceCommand(
            ICommandRepository<Workspace> workspaceRepository,
            IQueryRepository<Workspace> workspaceQueryRepository,
            IMapper mapper,
            IUserWorkspaceRepository userWorkspaceRepository,
            IResultFactory resultFactory)
        {
            _workspaceRepository = workspaceRepository ?? throw new ArgumentNullException(nameof(workspaceRepository));
            _workspaceQueryRepository = workspaceQueryRepository ?? throw new ArgumentNullException(nameof(workspaceQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateWorkspaceResponseModel>> Update(int id, UpdateWorkspaceModel model, int userId)
        {
            var workspace = await _workspaceQueryRepository.GetByIdAsync(id);

            if (workspace == null)
            {
                return _resultFactory.Failure<UpdateWorkspaceResponseModel>("Workspace not found.");
            }

            var isOwner = await _userWorkspaceRepository.IsOwnerOfWorkspace(userId, id);
            if (!isOwner)
            {
                return _resultFactory.Failure<UpdateWorkspaceResponseModel>("Solo el propietario puede modificar el workspace.");
            }

            _mapper.Map(model, workspace);

            await _workspaceRepository.UpdateAsync(workspace);

            var updatedWorkspaceResponse = _mapper.Map<UpdateWorkspaceResponseModel>(workspace);
            return _resultFactory.Success(updatedWorkspaceResponse);
        }
    }
}