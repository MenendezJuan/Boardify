﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IUpdateUserCommand<TModel, TResponse> where TModel : class where TResponse : class
    {
        Task<IResult<TResponse>> Update(string accessToken, TModel model);
    }
}