namespace Boardify.Application.Features.Boards.Models
{
    public class GetUserBoardsByWorkspaceQueryResult
    {
        public List<WorkspaceInfoModel> UserWorkspaces { get; set; } = new List<WorkspaceInfoModel>();
        public List<WorkspaceInfoModel> GuestWorkspaces { get; set; } = new List<WorkspaceInfoModel>();
    }
}