﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Users.Models;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IUpdatePasswordCommand
    {
        Task<IResult<bool>> UpdatePassword(string accessToken, UpdatePasswordModel model);
    }
}