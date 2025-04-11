namespace Boardify.Application.Features.Boards.Models
{
    public class RemoveLabelFromBoardModel
    {
        public int BoardId { get; set; }
        public int BoardLabelId { get; set; }
    }
}