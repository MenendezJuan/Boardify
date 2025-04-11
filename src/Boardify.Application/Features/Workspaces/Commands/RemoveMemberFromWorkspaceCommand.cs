﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Workspaces.Commands
{
    public class RemoveMemberFromWorkspaceCommand : IRemoveUserWorkspaceCommand
    {
        private readonly ICommandRepository<UserWorkspace> _userWorkspaceRepository;
        private readonly IQueryRepository<UserWorkspace> _userWorkspaceQueryRepository;
        private readonly IResultFactory _resultFactory;

        public RemoveMemberFromWorkspaceCommand(
            ICommandRepository<UserWorkspace> userWorkspaceRepository,
            IQueryRepository<UserWorkspace> userWorkspaceQueryRepository,
            IResultFactory resultFactory)
        {
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _userWorkspaceQueryRepository = userWorkspaceQueryRepository ?? throw new ArgumentNullException(nameof(userWorkspaceQueryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(RemoveMemberFromWorkspaceModel model)
        {
            var userWorkspace = await _userWorkspaceQueryRepository.GetFirstOrDefaultAsync(uw => uw.UserId == model.UserId && uw.WorkspaceId == model.WorkspaceId);

            if (userWorkspace == null)
            {
                return _resultFactory.Failure<bool>("El miembro del workspace no fue encontrado.");
            }

            if (userWorkspace.IsOwner)
            {
                return _resultFactory.Failure<bool>("El propietario del workspace no puede ser eliminado.");
            }

            await _userWorkspaceRepository.DeleteAsync(userWorkspace);

            return _resultFactory.Success(true);
        }
    }
}