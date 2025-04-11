﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IAddLabelToBoardCommand
    {
        Task<IResult<AddLabelToBoardResponseModel>> Create(AddLabelToBoardModel model, int creatorUserId);
    }
}