namespace Boardify.Application.Features.Workspaces.Models
{
    public class RemoveMemberFromWorkspaceModel
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
    }
}