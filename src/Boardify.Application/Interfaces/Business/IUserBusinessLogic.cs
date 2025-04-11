﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Application.Interfaces.Business
{
    public interface IUserBusinessLogic
    {
        #region UserCommands

        Task<IResult<CreateUserResponseModel>> CreateUser(CreateUserModel createUserModel);

        Task<IResult<UpdateUserResponseModel>> UpdateUser(string accessToken, UpdateUserModel updateUserModel);

        Task<IResult<bool>> DeleteUser(int userId);

        Task<IResult<bool>> UpdatePassword(string accessToken, UpdatePasswordModel model);

        #endregion UserCommands

        #region UserQueries

        Task<IResult<GetUserByEmailAddressModel>> GetUserByEmailAddress(string emailAddress);

        Task<IResult<GetUserByIdModel>> GetUserById(int userId);

        Task<IResult<IEnumerable<SearchUserModel>>> SearchUsersAsync(string searchText);

        Task<IResult<LoginResultModel>> Login(LoginModel loginModel);

        Task<IResult<LoginResultModel>> RevalidateToken(string refreshToken);

        Task<IResult<IActionResult>> GetImageResultAsync(string relativeUrl);

        #endregion UserQueries

        #region UserWorkspaceQueries

        Task<IResult<IEnumerable<UserWorkspacesModel>>> GetUserWorkspacesAsync(int userId);

        #endregion UserWorkspaceQueries
    }
}