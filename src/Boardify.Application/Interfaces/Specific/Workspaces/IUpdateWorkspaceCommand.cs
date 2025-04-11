﻿using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.Workspaces
{
    public interface IUpdateWorkspaceCommand<TModel, TResponse> where TModel : class where TResponse : class
    {
        Task<IResult<TResponse>> Update(int id, TModel model, int userId);
    }
}