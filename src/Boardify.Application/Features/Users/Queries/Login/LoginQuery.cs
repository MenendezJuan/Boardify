using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Specific.Users;
using Microsoft.IdentityModel.Tokens;

namespace Boardify.Application.Features.Users.Queries.Login
{
    public class LoginQuery : ILoginQuery
    {
        private readonly IMapper _mapper;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IPasswordServicio _passwordService;
        private readonly ITokenHandler _tokenJwtService;
        private readonly IResultFactory _resultFactory;

        public LoginQuery(IMapper mapper, IUserQueryRepository userQueryRepository, IPasswordServicio passwordService, ITokenHandler tokenJwtService, IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _tokenJwtService = tokenJwtService ?? throw new ArgumentNullException(nameof(tokenJwtService));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<LoginResultModel>> Handle(LoginModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return _resultFactory.Failure<LoginResultModel>("Email cannot be null or empty.");
            }

            var user = await _userQueryRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return _resultFactory.Failure<LoginResultModel>("Usuario no encontrado o correo incorrecto.");
            }

            if (user.Password == null || request.Password == null || user.Salt == null)
            {
                return _resultFactory.Failure<LoginResultModel>("Error en los datos de entrada.");
            }

            if (!_passwordService.Check(user.Password, request.Password, user.Salt))
            {
                return _resultFactory.Failure<LoginResultModel>("Contraseña incorrecta.");
            }

            try
            {
                var accessToken = _tokenJwtService.CreateAccessToken(user);
                var refreshToken = _tokenJwtService.CreateRefreshToken(user);

                var resultModel = new LoginResultModel
                {
                    Token = accessToken.AccessToken,
                    RefreshToken = refreshToken,
                    User = _mapper.Map<UserModel>(user)
                };

                return _resultFactory.Success(resultModel);
            }
            catch (SecurityTokenException ex)
            {
                return _resultFactory.Failure<LoginResultModel>($"Error al generar el token de acceso: {ex.Message}");
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<LoginResultModel>($"Error interno: {ex.Message}");
            }
        }
    }
}