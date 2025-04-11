﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Generics
{
    public interface IUpdateCommand<TModel, TResponse> where TModel : class where TResponse : class
    {
        Task<IResult<TResponse>> Update(TModel model);
    }
}