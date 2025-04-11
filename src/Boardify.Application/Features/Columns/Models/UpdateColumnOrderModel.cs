namespace Boardify.Application.Features.Columns.Models
{
    public class UpdateColumnOrderModel
    {
        public int BoardId { get; set; }
        public List<int> ColumnOrder { get; set; } = new List<int>();
    }
}