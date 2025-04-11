using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync
{
    public class BoardResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public VisibilityEnum IdVisibility { get; set; }
        public int WorkspaceId { get; set; }
        public int MemberCount { get; set; }
        public CompleteBoardDto CompleteBoard { get; set; } = null!;

    }
}