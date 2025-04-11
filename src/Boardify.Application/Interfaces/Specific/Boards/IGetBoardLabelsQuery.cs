﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IGetBoardLabelsQuery
    {
        Task<IResult<IEnumerable<BoardLabelModel>>> GetBoardLabelsAsync(int boardId);
    }
}