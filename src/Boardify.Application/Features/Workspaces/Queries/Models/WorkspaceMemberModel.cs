namespace Boardify.Application.Features.Workspaces.Queries.Models
{
    public class WorkspaceMemberModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? LastName { get; set; }
    }
}