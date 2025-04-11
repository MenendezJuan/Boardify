using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Application.Interfaces.Specific.Workspaces;

namespace Boardify.Application.Features.Workspaces.Queries
{
    public class GetOwnedWorkspacesQuery : IGetOwnedWorkspaceQuery
    {
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetOwnedWorkspacesQuery(IUserWorkspaceRepository userWorkspaceRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<GetOwnedWorkspacesModel>>> Execute(int userId)
        {
            try
            {
                var workspaces = await _userWorkspaceRepository.GetOwnedWorkspacesAsync(userId);

                if (workspaces == null || !workspaces.Any())
                {
                    return _resultFactory.Failure<IEnumerable<GetOwnedWorkspacesModel>>("No se encontraron espacios de trabajo para el usuario.");
                }

                var workspaceModels = _mapper.Map<IEnumerable<GetOwnedWorkspacesModel>>(workspaces);
                return _resultFactory.Success(workspaceModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<GetOwnedWorkspacesModel>>($"Error interno: {ex.Message}");
            }
        }
    }
}