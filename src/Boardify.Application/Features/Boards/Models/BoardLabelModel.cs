namespace Boardify.Application.Features.Boards.Models
{
    public class BoardLabelModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
    }
}