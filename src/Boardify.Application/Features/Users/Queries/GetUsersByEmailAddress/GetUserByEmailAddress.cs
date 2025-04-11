using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces.Specific.Users;

namespace Boardify.Application.Features.Users.Queries.GetUsersByEmailAddress
{
    public class GetUserByEmailAddress : IGetUserByEmailQuery
    {
        private readonly IUserQueryRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetUserByEmailAddress(IUserQueryRepository userRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<GetUserByEmailAddressModel>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user != null)
            {
                return _resultFactory.Failure<GetUserByEmailAddressModel>(new Dictionary<string, string>
                {
                    { "Email", "El correo ya se encuentra registrado." }
                });
            }

            var userModel = _mapper.Map<GetUserByEmailAddressModel>(user);
            return _resultFactory.Success(userModel);
        }
    }
}