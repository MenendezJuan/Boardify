using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Application.Interfaces.Specific.Workspaces;

namespace Boardify.Application.Features.Workspaces.Queries
{
    public class GetWorkspaceMembersQuery : IGetWorkspaceMembersQuery
    {
        private readonly IMapper _mapper;
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IResultFactory _resultFactory;

        public GetWorkspaceMembersQuery(IMapper mapper, IUserWorkspaceRepository userWorkspaceRepository, IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<WorkspaceMemberModel>>> GetWorkspaceMembersAsync(int workspaceId)
        {
            try
            {
                var userWorkspaces = await _userWorkspaceRepository.GetWorkspaceMembers(workspaceId);

                if (userWorkspaces == null || !userWorkspaces.Any())
                {
                    return _resultFactory.Failure<IEnumerable<WorkspaceMemberModel>>("No se encontraron miembros para el workspace.");
                }

                var users = userWorkspaces.Select(uw => uw.User).Where(u => u != null);

                var workspaceMemberModels = _mapper.Map<IEnumerable<WorkspaceMemberModel>>(users);

                return _resultFactory.Success(workspaceMemberModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<WorkspaceMemberModel>>($"Error interno: {ex.Message}");
            }
        }
    }
}