﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.Testing;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IGetUserBoardsByWorkspaceQuery
    {
        Task<IResult<GetUserBoardsByWorkspaceQueryResult>> ExecuteAsync(GetUserBoardsByWorkspaceQueryModel model);
    }
}