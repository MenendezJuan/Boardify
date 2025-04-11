using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Workspaces.Commands
{
    public class AddMemberToWorkspaceCommand : IAddMemberToWorkspaceCommand
    {
        private readonly ICommandRepository<UserWorkspace> _userWorkspaceRepository;
        private readonly IQueryRepository<UserWorkspace> _userWorkspaceQueryRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IQueryRepository<Workspace> _workspaceQueryRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceRepositorySpecific;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public AddMemberToWorkspaceCommand(
            ICommandRepository<UserWorkspace> userWorkspaceRepository,
            IQueryRepository<User> userQueryRepository,
            IQueryRepository<Workspace> workspaceQueryRepository,
            IMapper mapper,
            IQueryRepository<UserWorkspace> userWorkspaceQueryRepository,
            IUserWorkspaceRepository userWorkspaceRepositorySpecific,
            IResultFactory resultFactory)
        {
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _workspaceQueryRepository = workspaceQueryRepository ?? throw new ArgumentNullException(nameof(workspaceQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userWorkspaceQueryRepository = userWorkspaceQueryRepository ?? throw new ArgumentNullException(nameof(userWorkspaceQueryRepository));
            _userWorkspaceRepositorySpecific = userWorkspaceRepositorySpecific ?? throw new ArgumentNullException(nameof(userWorkspaceRepositorySpecific));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<AddMemberToWorkspaceResponseModel>> Create(AddMemberToWorkspaceModel model, int creatorUserId)
        {
            var user = await _userQueryRepository.GetByIdAsync(model.UserId);
            var workspace = await _workspaceQueryRepository.GetByIdAsync(model.WorkspaceId);

            if (user == null || workspace == null)
            {
                return _resultFactory.Failure<AddMemberToWorkspaceResponseModel>("El usuario o el workspace no existen.");
            }

            var isOwner = await _userWorkspaceRepositorySpecific.IsOwnerOfWorkspace(creatorUserId, model.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<AddMemberToWorkspaceResponseModel>("Solo el propietario puede agregar miembros al workspace.");
            }

            var existingMembership = await _userWorkspaceQueryRepository.GetFirstOrDefaultAsync(uw => uw.UserId == model.UserId && uw.WorkspaceId == model.WorkspaceId);

            if (existingMembership != null)
            {
                return _resultFactory.Failure<AddMemberToWorkspaceResponseModel>("El usuario ya es miembro del workspace.");
            }

            var userWorkspace = _mapper.Map<UserWorkspace>(model);

            await _userWorkspaceRepository.InsertAsync(userWorkspace);

            var responseModel = _mapper.Map<AddMemberToWorkspaceResponseModel>(userWorkspace);

            return _resultFactory.Success(responseModel);
        }
    }
}