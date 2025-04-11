using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Users.Commands
{
    public class CreateUserCommand : ICreateCommand<CreateUserModel, CreateUserResponseModel>
    {
        private readonly ICommandRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordServicio _passwordServicio;
        private readonly IGetUserByEmailQuery _getUserByEmailQuery;
        private readonly ITokenHandler _tokenJwtService;
        private readonly IResultFactory _resultFactory;

        public CreateUserCommand(
            ICommandRepository<User> userRepository,
            IMapper mapper,
            IPasswordServicio passwordServicio,
            IGetUserByEmailQuery getUserByEmailQuery,
            ITokenHandler tokenJwtService,
            IResultFactory resultFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordServicio = passwordServicio ?? throw new ArgumentNullException(nameof(passwordServicio));
            _getUserByEmailQuery = getUserByEmailQuery ?? throw new ArgumentNullException(nameof(getUserByEmailQuery));
            _tokenJwtService = tokenJwtService ?? throw new ArgumentNullException(nameof(tokenJwtService));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CreateUserResponseModel>> Create(CreateUserModel model)
        {
            var userEntity = _mapper.Map<User>(model);
            userEntity.Email = userEntity.Email?.ToLower();
            userEntity.Status = StatusEnum.Active;

            string password = userEntity.Password ?? string.Empty;

            (userEntity.Password, userEntity.Salt) = _passwordServicio.HashWithSalt(password);

            string email = userEntity.Email ?? string.Empty;

            var existingUser = await _getUserByEmailQuery.GetUserByEmailAsync(email);
            if (existingUser.Status != ResultStatus.Success)
            {
                return _resultFactory.Failure<CreateUserResponseModel>(new Dictionary<string, string>
                {
                    { "Email", "El correo electrónico ya está registrado." }
                });
            }

            await _userRepository.InsertAsync(userEntity);

            var token = _tokenJwtService.CreateAccessToken(userEntity).AccessToken;

            var responseModel = _mapper.Map<CreateUserResponseModel>(userEntity);
            responseModel.Token = token;

            return _resultFactory.Success(responseModel);
        }
    }
}