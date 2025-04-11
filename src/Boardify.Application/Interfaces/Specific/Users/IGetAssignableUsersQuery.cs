﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Users.Models;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IGetAssignableUsersQuery
    {
        Task<IResult<List<UserResponseModel>>> GetAssignableUsers(int cardId);
    }
}