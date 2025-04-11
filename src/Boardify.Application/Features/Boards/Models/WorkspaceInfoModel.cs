using Boardify.Application.Features.Boards.Models.Testing;

namespace Boardify.Application.Features.Boards.Models
{
    public class WorkspaceInfoModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MemberCount { get; set; }
        public List<BoardInfoModel> Boards { get; set; } = new List<BoardInfoModel>();
    }
}