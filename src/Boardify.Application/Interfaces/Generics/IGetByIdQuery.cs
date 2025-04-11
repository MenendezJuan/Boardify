﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Generics
{
    public interface IGetByIdQuery<TModel, TKey>
    {
        Task<IResult<TModel>> GetByIdAsync(TKey id);
    }
}