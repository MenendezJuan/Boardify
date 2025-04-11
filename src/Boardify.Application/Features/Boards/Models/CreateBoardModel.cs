using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Models
{
    public class CreateBoardModel
    {
        public int WorkspaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public VisibilityEnum Visibility { get; set; }
    }
}