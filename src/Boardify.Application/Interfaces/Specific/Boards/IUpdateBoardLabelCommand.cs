﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IUpdateBoardLabelCommand<UpdateBoardLabelModel, UpdateBoardLabelResponseModel>
    {
        Task<IResult<UpdateBoardLabelResponseModel>> Update(int boardLabelId, UpdateBoardLabelModel model, int userId);
    }
}