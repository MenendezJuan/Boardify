namespace Boardify.Application.Features.Boards.Models
{
    public class AddLabelToBoardModel
    {
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public int BoardId { get; set; }
    }
}