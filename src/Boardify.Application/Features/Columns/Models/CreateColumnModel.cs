namespace Boardify.Application.Features.Columns.Models
{
    public class CreateColumnModel
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}