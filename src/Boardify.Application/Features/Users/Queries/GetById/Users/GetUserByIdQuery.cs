using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Users.Queries.GetById.Users
{
    public class GetUserByIdQuery : IGetByIdQuery<GetUserByIdModel, int>
    {
        private readonly IMapper _mapper;
        private readonly IQueryRepository<User> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public GetUserByIdQuery(IMapper mapper, IQueryRepository<User> queryRepository, IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<GetUserByIdModel>> GetByIdAsync(int userId)
        {
            var user = await _queryRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return _resultFactory.Failure<GetUserByIdModel>($"User with ID {userId} not found.");
            }

            var userDto = _mapper.Map<GetUserByIdModel>(user);
            return _resultFactory.Success(userDto);
        }
    }
}