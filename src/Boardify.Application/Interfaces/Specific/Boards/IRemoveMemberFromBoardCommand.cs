﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IRemoveMemberFromBoardCommand
    {
        Task<IResult<bool>> RemoveAsync(RemoveMemberFromBoardModel model, int requesterUserId);
    }
}