﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IGetBoardMembersQuery
    {
        Task<IResult<IEnumerable<BoardMemberModel>>> GetBoardMembersAsync(int boardId);
    }
}