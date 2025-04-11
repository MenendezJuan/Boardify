﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Workspaces.Queries.Models;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IGetWorkspaceMembersQuery
    {
        Task<IResult<IEnumerable<WorkspaceMemberModel>>> GetWorkspaceMembersAsync(int workspaceId);
    }
}