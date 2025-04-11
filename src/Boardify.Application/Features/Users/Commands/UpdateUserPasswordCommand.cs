﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Users.Commands
{
    public class UpdateUserPasswordCommand : IUpdatePasswordCommand
    {
        private readonly ICommandRepository<User> _userRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IPasswordServicio _passwordServicio;
        private readonly ITokenHandler _tokenHandler;
        private readonly IResultFactory _resultFactory;
        public UpdateUserPasswordCommand(ICommandRepository<User> userRepository,
                                         IQueryRepository<User> userQueryRepository,
                                         IPasswordServicio passwordServicio,
                                         ITokenHandler tokenHandler,
                                         IResultFactory resultFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _passwordServicio = passwordServicio ?? throw new ArgumentNullException(nameof(passwordServicio));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> UpdatePassword(string accessToken, UpdatePasswordModel model)
        {
            var userIdString = _tokenHandler.GetUserIdFromAccessToken(accessToken);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return _resultFactory.Failure<bool>("Invalid access token.");
            }

            var user = await _userQueryRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return _resultFactory.Failure<bool>("El usuario no existe.");
            }

            string salt = user.Salt ?? string.Empty;
            string password = user.Password ?? string.Empty;
            string currentPassword = model.CurrentPassword ?? string.Empty;

            if (!_passwordServicio.Check(password, currentPassword, salt))
            {
                return _resultFactory.Failure<bool>("La contraseña actual es incorrecta.");
            }

            string newPassword = model.NewPassword ?? string.Empty;

            (user.Password, user.Salt) = _passwordServicio.HashWithSalt(newPassword);

            await _userRepository.UpdateAsync(user);

            return _resultFactory.Success(true);
        }
    }
}