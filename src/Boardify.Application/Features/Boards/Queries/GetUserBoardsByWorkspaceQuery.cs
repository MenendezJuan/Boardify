using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.Testing;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Queries
{
    public class GetUserBoardsByWorkspaceQuery : IGetUserBoardsByWorkspaceQuery
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetUserBoardsByWorkspaceQuery(IWorkspaceRepository workspaceRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _workspaceRepository = workspaceRepository ?? throw new ArgumentNullException(nameof(workspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<GetUserBoardsByWorkspaceQueryResult>> ExecuteAsync(GetUserBoardsByWorkspaceQueryModel model)
        {
            var userId = model.UserId;
            var result = new GetUserBoardsByWorkspaceQueryResult();

            var userWorkspaces = (await _workspaceRepository.GetUserWorkspacesAsync(userId)) ?? Enumerable.Empty<Workspace>();
            var guestWorkspaces = (await _workspaceRepository.GetGuestWorkspacesAsync(userId)) ?? Enumerable.Empty<Workspace>();

            var allWorkspaceIds = userWorkspaces
                .Concat(guestWorkspaces)
                .Select(w => w.Id)
                .ToList();

            var workspaceMemberCounts = await _workspaceRepository.GetWorkspaceMemberCountsAsync(allWorkspaceIds);

            var workspaceInfos = userWorkspaces
                .Select(w =>
                {
                    var workspaceInfo = _mapper.Map<WorkspaceInfoModel>(w);
                    workspaceInfo.MemberCount = workspaceMemberCounts.ContainsKey(w.Id) ? workspaceMemberCounts[w.Id] : 0;
                    workspaceInfo.Boards = w.Boards
                        .Where(b => b.Visibility == VisibilityEnum.Workspace ||
                                    b.Visibility == VisibilityEnum.Public ||
                                    (b.Visibility == VisibilityEnum.Private && b.BoardMembers.Any(bm => bm.UserId == userId)))
                        .Select(b => _mapper.Map<BoardInfoModel>(b)).ToList();
                    return workspaceInfo;
                }).ToList();

            var guestWorkspaceInfos = guestWorkspaces
                .Select(w =>
                {
                    var workspaceInfo = _mapper.Map<WorkspaceInfoModel>(w);
                    workspaceInfo.MemberCount = workspaceMemberCounts.ContainsKey(w.Id) ? workspaceMemberCounts[w.Id] : 0;
                    workspaceInfo.Boards = w.Boards
                        .Where(b => b.Visibility == VisibilityEnum.Workspace ||
                                    b.Visibility == VisibilityEnum.Public ||
                                    (b.Visibility == VisibilityEnum.Private && b.BoardMembers.Any(bm => bm.UserId == userId)))
                        .Select(b => _mapper.Map<BoardInfoModel>(b)).ToList();
                    return workspaceInfo;
                }).ToList();

            result.UserWorkspaces.AddRange(workspaceInfos);
            result.GuestWorkspaces.AddRange(guestWorkspaceInfos);

            return _resultFactory.Success(result);
        }
    }
}