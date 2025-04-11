﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Generics
{
    public interface IGetAllQuery<TModel>
    {
        Task<IResult<IEnumerable<TModel>>> GetAllAsync();
    }
}