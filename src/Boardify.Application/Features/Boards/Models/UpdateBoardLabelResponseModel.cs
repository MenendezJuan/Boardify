namespace Boardify.Application.Features.Boards.Models
{
    public class UpdateBoardLabelResponseModel
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
    }
}