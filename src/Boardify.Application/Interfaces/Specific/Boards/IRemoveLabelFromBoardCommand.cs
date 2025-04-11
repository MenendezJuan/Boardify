﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IRemoveLabelFromBoardCommand
    {
        Task<IResult<bool>> RemoveAsync(RemoveLabelFromBoardModel model, int requesterUserId);
    }
}