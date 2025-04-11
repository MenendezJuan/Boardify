﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Users.Queries.Models;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface ILoginQuery
    {
        Task<IResult<LoginResultModel>> Handle(LoginModel request);
    }
}