﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Generics
{
    public interface ICreateCommand<TModel, TResponse>
    {
        Task<IResult<TResponse>> Create(TModel model);
    }
}