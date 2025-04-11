using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Utils;
using Boardify.Domain.Entities;
using FluentValidation;

namespace Boardify.Application.Features.Workspaces.Commands.CommandValidators
{
    public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceModel>
    {
        private readonly IQueryRepository<Workspace> _workspaceQueryRepository;

        public CreateWorkspaceCommandValidator(IQueryRepository<Workspace> workspaceQueryRepository)
        {
            _workspaceQueryRepository = workspaceQueryRepository;

            RuleFor(x => x.Name)
                         .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                         .MaximumLength(72).WithMessage(ValidatorsUtils.MaximumLengthMessage)
                         .MustAsync(async (name, cancellation) =>
                             (await _workspaceQueryRepository.GetFirstOrDefaultAsync(ws => ws.Name == name)) == null)
                         .WithMessage("Ya existe un workspace con el mismo nombre.")
                         .Matches("^[a-zA-Z0-9 ]+$").WithMessage("El nombre del workspace solo puede contener caracteres alfanuméricos y espacios.");
        }
    }
}