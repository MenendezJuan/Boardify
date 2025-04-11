﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IPermissionCheckQuery
    {
        Task<IResult<PermissionCheckResponseModel>> CheckPermissions(int boardId, int userId);
    }
}