namespace Boardify.Application.Features.Columns.Models
{
    public class CreateColumnResponseModel
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }
    }
}