namespace Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync
{
    public class CompleteBoardDto
    {
        public Dictionary<int, ColumnDto> Columns { get; set; } = new Dictionary<int, ColumnDto>();
        public Dictionary<int, CardDto> Cards { get; set; } = new Dictionary<int, CardDto>();
        public List<int> ColumnOrder { get; set; } = new List<int>();
    }
}