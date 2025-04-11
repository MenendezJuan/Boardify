namespace Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public List<int> CardIds { get; set; } = new List<int>();
    }
}