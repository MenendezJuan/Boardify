﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IAddMemberToBoardCommand
    {
        Task<IResult<AddMemberToBoardResponseModel>> Create(AddMemberToBoardModel model, int creatorUserId);
    }
}