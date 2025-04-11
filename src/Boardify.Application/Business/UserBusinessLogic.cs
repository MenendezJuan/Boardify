using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Application.Business
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly IValidator<CreateUserModel> _createUserValidator;
        private readonly IValidator<UpdateUserModel> _updateUserValidator;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly IValidator<GetUserByEmailAddressModel> _getUserByEmailAddressValidator;
        private readonly ICreateCommand<CreateUserModel, CreateUserResponseModel> _createUserCommand;
        private readonly IUpdateUserCommand<UpdateUserModel, UpdateUserResponseModel> _updateUserCommand;
        private readonly IDeleteCommand<User> _deleteUserCommand;
        private readonly IGetByIdQuery<GetUserByIdModel, int> _getByIdQuery;
        private readonly IGetUserByEmailQuery _getUserByEmailQuery;
        private readonly IUpdatePasswordCommand _updateUserPasswordCommand;
        private readonly ILoginQuery _loginQuery;
        private readonly IGetTokenRevalidationQuery _tokenRevalidationQuery;
        private readonly IGetUserWorkspacesQuery _getUserWorkspacesQuery;
        private readonly ISearchUsersQuery _searchUsersQuery;
        private readonly IGetImageQuery _getImageQuery;
        private readonly IValidator<UpdatePasswordModel> _updatePasswordValidator;
        private readonly IResultFactory _resultFactory;

        public UserBusinessLogic(
            ICreateCommand<CreateUserModel, CreateUserResponseModel> createUserCommand,
            IUpdateUserCommand<UpdateUserModel, UpdateUserResponseModel> updateUserCommand,
            IDeleteCommand<User> deleteUserCommand,
            IGetByIdQuery<GetUserByIdModel, int> getByIdQuery,
            IGetUserByEmailQuery getUserByEmailQuery,
            IUpdatePasswordCommand updateUserPasswordCommand,
            ILoginQuery loginQuery,
            IGetTokenRevalidationQuery tokenRevalidationQuery,
            IGetUserWorkspacesQuery getUserWorkspacesQuery,
            ISearchUsersQuery searchUsersQuery,
            IValidator<CreateUserModel> createUserValidator,
            IValidator<LoginModel> loginValidator,
            IValidator<UpdateUserModel> updateUserValidator,
            IValidator<GetUserByEmailAddressModel> getUserByEmailAddressValidator,
            IGetImageQuery getImageQuery,
            IValidator<UpdatePasswordModel> updatePasswordValidator,
            IResultFactory resultFactory)
        {
            _createUserCommand = createUserCommand ?? throw new ArgumentNullException(nameof(createUserCommand));
            _updateUserCommand = updateUserCommand ?? throw new ArgumentNullException(nameof(updateUserCommand));
            _deleteUserCommand = deleteUserCommand ?? throw new ArgumentNullException(nameof(deleteUserCommand));
            _getByIdQuery = getByIdQuery ?? throw new ArgumentNullException(nameof(getByIdQuery));
            _getUserByEmailQuery = getUserByEmailQuery ?? throw new ArgumentNullException(nameof(getUserByEmailQuery));
            _updateUserPasswordCommand = updateUserPasswordCommand ?? throw new ArgumentNullException(nameof(updateUserPasswordCommand));
            _loginQuery = loginQuery ?? throw new ArgumentNullException(nameof(loginQuery));
            _tokenRevalidationQuery = tokenRevalidationQuery ?? throw new ArgumentNullException(nameof(tokenRevalidationQuery));
            _getUserWorkspacesQuery = getUserWorkspacesQuery ?? throw new ArgumentNullException(nameof(getUserWorkspacesQuery));
            _searchUsersQuery = searchUsersQuery ?? throw new ArgumentNullException(nameof(searchUsersQuery));
            _createUserValidator = createUserValidator ?? throw new ArgumentNullException(nameof(createUserValidator));
            _updateUserValidator = updateUserValidator ?? throw new ArgumentNullException(nameof(updateUserValidator));
            _loginValidator = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
            _getUserByEmailAddressValidator = getUserByEmailAddressValidator ?? throw new ArgumentNullException(nameof(getUserByEmailAddressValidator));
            _getImageQuery = getImageQuery ?? throw new ArgumentNullException(nameof(getImageQuery));
            _updatePasswordValidator = updatePasswordValidator;
            _resultFactory = resultFactory;
        }

        #region UserBL

        public async Task<IResult<IActionResult>> GetImageResultAsync(string relativeUrl)
        {
            try
            {
                var imagePath = await _getImageQuery.GetImageAsync(relativeUrl);

                var fileExtension = Path.GetExtension(imagePath).TrimStart('.');
                var mimeType = $"image/{fileExtension}";

                return _resultFactory.Success<IActionResult>(new PhysicalFileResult(imagePath, mimeType));
            }
            catch (NotFoundException)
            {
                return _resultFactory.Failure<IActionResult>(new Dictionary<string, string> { { "Error", "Image not found." } });
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IActionResult>(new Dictionary<string, string> { { "Error", ex.Message } });
            }
        }

        public async Task<IResult<LoginResultModel>> RevalidateToken(string refreshToken)
        {
            return await _tokenRevalidationQuery.RevalidateToken(refreshToken);
        }

        public async Task<IResult<CreateUserResponseModel>> CreateUser(CreateUserModel createUserModel)
        {
            var validation = await _createUserValidator.ValidateAsync(createUserModel);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<CreateUserResponseModel>(errorDictionary);
            }

            return await _createUserCommand.Create(createUserModel);
        }

        public async Task<IResult<bool>> DeleteUser(int userId)
        {
            var user = await _getByIdQuery.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("Usuario", userId);
            }

                return await _deleteUserCommand.Delete(userId);
        }

        public async Task<IResult<GetUserByEmailAddressModel>> GetUserByEmailAddress(string emailAddress)
        {
            var validation = await _getUserByEmailAddressValidator.ValidateAsync(new GetUserByEmailAddressModel { Email = emailAddress });

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<GetUserByEmailAddressModel>(errorDictionary);
            }

            return await _getUserByEmailQuery.GetUserByEmailAsync(emailAddress);

        }
        public async Task<IResult<GetUserByIdModel>> GetUserById(int userId)
        {
            var user = await _getByIdQuery.GetByIdAsync(userId);

            if (user == null)
            {
                return _resultFactory.Failure<GetUserByIdModel>($"User with ID {userId} not found.");
            }

            return user;
        }

        public async Task<IResult<UpdateUserResponseModel>> UpdateUser(string accessToken, UpdateUserModel updateUserModel)
        {
            var validation = await _updateUserValidator.ValidateAsync(updateUserModel);

            if (!validation.IsValid)
            {
                return _resultFactory.Failure<UpdateUserResponseModel>(validation.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
            }

            return await _updateUserCommand.Update(accessToken, updateUserModel);
        }

        public async Task<IResult<bool>> UpdatePassword(string accessToken, UpdatePasswordModel model)
        {
            var validationResult = await _updatePasswordValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errorDictionary = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<bool>(errorDictionary);
            }

            return await _updateUserPasswordCommand.UpdatePassword(accessToken, model);
        }

        public async Task<IResult<LoginResultModel>> Login(LoginModel loginModel)
        {
            var validation = await _loginValidator.ValidateAsync(loginModel);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<LoginResultModel>(errorDictionary);
            }

            var result = await _loginQuery.Handle(loginModel);

            if (!result.IsSuccess())
            {
                return result;
            }

            return result;
        }

        public async Task<IResult<IEnumerable<SearchUserModel>>> SearchUsersAsync(string searchText)
        {
            return await _searchUsersQuery.SearchUsersAsync(searchText);
        }

        #endregion UserBL

        #region UserWorkspace

        public async Task<IResult<IEnumerable<UserWorkspacesModel>>> GetUserWorkspacesAsync(int userId)
        {
            return await _getUserWorkspacesQuery.GetUserWorkspacesAsync(userId);
        }

        #endregion UserWorkspace
    }
}