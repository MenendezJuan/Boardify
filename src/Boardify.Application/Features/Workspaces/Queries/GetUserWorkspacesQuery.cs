using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Application.Interfaces.Specific.Workspaces;

namespace Boardify.Application.Features.Workspaces.Queries
{
    public class GetUserWorkspacesQuery : IGetUserWorkspacesQuery
    {
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetUserWorkspacesQuery(IUserWorkspaceRepository userWorkspaceRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<UserWorkspacesModel>>> GetUserWorkspacesAsync(int userId)
        {
            try
            {
                var userWorkspaces = await _userWorkspaceRepository.GetUserWorkspacesAsync(userId);

                if (userWorkspaces == null || !userWorkspaces.Any())
                {
                    return _resultFactory.Failure<IEnumerable<UserWorkspacesModel>>("No workspaces found for the user.");
                }

                var workspaceDtos = _mapper.Map<IEnumerable<UserWorkspacesModel>>(userWorkspaces.Select(uw => uw.Workspace).Where(w => w != null));

                return _resultFactory.Success(workspaceDtos);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<UserWorkspacesModel>>($"An error occurred while retrieving user workspaces: {ex.Message}");
            }
        }
    }
}