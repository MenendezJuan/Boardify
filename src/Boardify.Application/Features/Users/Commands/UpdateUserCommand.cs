using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Files;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IUpdateUserCommand<UpdateUserModel, UpdateUserResponseModel>
    {
        private readonly ICommandRepository<User> _userRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordServicio _passwordServicio;
        private readonly IGetUserByEmailQuery _getUserByEmailQuery;
        private readonly IFileService _fileService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IResultFactory _resultFactory;

        public UpdateUserCommand(
            ICommandRepository<User> userRepository,
            IQueryRepository<User> userQueryRepository,
            IMapper mapper,
            IPasswordServicio passwordServicio,
            IGetUserByEmailQuery getUserByEmailQuery,
            IFileService fileService,
            ITokenHandler tokenHandler,
            IResultFactory resultFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordServicio = passwordServicio ?? throw new ArgumentNullException(nameof(passwordServicio));
            _getUserByEmailQuery = getUserByEmailQuery ?? throw new ArgumentNullException(nameof(getUserByEmailQuery));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateUserResponseModel>> Update(string accessToken, UpdateUserModel model)
        {
            var userIdString = _tokenHandler.GetUserIdFromAccessToken(accessToken);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return _resultFactory.Failure<UpdateUserResponseModel>("Invalid access token.");
            }

            var user = await _userQueryRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return _resultFactory.Failure<UpdateUserResponseModel>("User not found.");
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.Email.ToLower();
            }

            var existingUserResult = await _getUserByEmailQuery.GetUserByEmailAsync(model.Email ?? string.Empty);

            if (existingUserResult.IsSuccess())
            {
                var existingUser = existingUserResult.SuccessValue;
                if (existingUser.Id != userId)
                {
                    return _resultFactory.Failure<UpdateUserResponseModel>("The email is already registered.");
                }
            }
            else
            {
                return _resultFactory.Failure<UpdateUserResponseModel>("Failed to check if email is registered.");
            }

            if (!string.IsNullOrEmpty(model.AvatarBase64))
            {
                if (string.IsNullOrEmpty(model.AvatarExtension))
                {
                    return _resultFactory.Failure<UpdateUserResponseModel>("Avatar extension is required.");
                }

                var avatarUrl = _fileService.SaveAvatarBase64(model.AvatarBase64, user.Id.ToString(), model.AvatarExtension);
                user.Avatar = avatarUrl;
            }
            else if (model.AvatarUrl == null && !string.IsNullOrEmpty(user.Avatar))
            {
                _fileService.DeleteAvatar(user.Id.ToString());
                user.Avatar = null;
            }

            _mapper.Map(model, user);
            user.LastModifiedTime = DateTime.Now;

            await _userRepository.UpdateAsync(user);

            var responseModel = new UpdateUserResponseModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Avatar = user.Avatar
            };

            return _resultFactory.Success(responseModel);
        }
    }
}