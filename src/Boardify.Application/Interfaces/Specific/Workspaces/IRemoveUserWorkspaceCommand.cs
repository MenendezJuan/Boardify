﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Workspaces.Models;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IRemoveUserWorkspaceCommand
    {
        Task<IResult<bool>> Delete(RemoveMemberFromWorkspaceModel model);
    }
}