﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface ICreateBoardCommand<TModel, TResponseModel>
    {
        Task<IResult<TResponseModel>> Create(TModel model, int creatorUserId);
    }
}