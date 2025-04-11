using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IDeleteCommand<User>
    {
        private readonly ICommandRepository<User> _userRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteUserCommand(ICommandRepository<User> userRepository, IQueryRepository<User> userQueryRepository, IResultFactory resultFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int userId)
        {
            var user = await _userQueryRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return _resultFactory.Failure<bool>("No se encontro el usuario");
            }

            user.Status = StatusEnum.Inactive;
            await _userRepository.UpdateAsync(user);

            return _resultFactory.Success(true);
        }
    }
}