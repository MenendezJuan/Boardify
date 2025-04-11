﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Workspaces.Queries.Models;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IGetUserWorkspacesQuery
    {
        Task<IResult<IEnumerable<UserWorkspacesModel>>> GetUserWorkspacesAsync(int userId);
    }
}