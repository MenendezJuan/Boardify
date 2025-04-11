﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Users.Queries.Models;

namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IGetUserByEmailQuery
    {
        Task<IResult<GetUserByEmailAddressModel>> GetUserByEmailAsync(string email);
    }
}