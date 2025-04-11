namespace Boardify.Application.Features.Columns.Models
{
    public class ColumnResponseModel
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
    }
}