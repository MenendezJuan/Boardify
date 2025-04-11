﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Workspaces.Models;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IAddMemberToWorkspaceCommand
    {
        Task<IResult<AddMemberToWorkspaceResponseModel>> Create(AddMemberToWorkspaceModel model, int creatorUserId);
    }
}