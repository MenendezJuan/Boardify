﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IUpdateBoardCommand<UpdateBoardModel, UpdateBoardResponseModel>
    {
        Task<IResult<UpdateBoardResponseModel>> Update(int boardId, UpdateBoardModel model, int userId);
    }
}