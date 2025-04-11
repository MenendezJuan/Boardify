﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;

namespace Boardify.Application.Interfaces.Business
{
    public interface IWorkspaceBusinessLogic
    {
        #region WorkspaceCommands

        Task<IResult<CreateWorkspaceResponseModel>> CreateWorkspace(CreateWorkspaceModel createWorkspaceModel, int creatorUserId);

        Task<IResult<UpdateWorkspaceResponseModel>> UpdateWorkspace(int id, UpdateWorkspaceModel updateWorkspace, int creatorUserId);

        Task<IResult<IEnumerable<GetOwnedWorkspacesModel>>> GetOwnedWorkspaces(int userId);

        #endregion WorkspaceCommands

        #region UserWorkspaceCommands

        Task<IResult<AddMemberToWorkspaceResponseModel>> AddMemberToWorkspace(AddMemberToWorkspaceModel model, int creatorUserId);

        Task<IResult<bool>> RemoveMemberFromWorkspace(RemoveMemberFromWorkspaceModel removeMemberFromWorkspace);

        Task<IResult<IEnumerable<WorkspaceMemberModel>>> GetWorkspaceMembersAsync(int workspaceId);

        #endregion UserWorkspaceCommands
    }
}