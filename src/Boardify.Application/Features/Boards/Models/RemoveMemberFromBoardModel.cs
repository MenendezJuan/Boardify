namespace Boardify.Application.Features.Boards.Models
{
    public class RemoveMemberFromBoardModel
    {
        public int BoardId { get; set; }
        public int UserId { get; set; }
    }
}