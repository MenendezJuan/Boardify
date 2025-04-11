namespace Boardify.Application.Features.Boards.Models
{
    public class BoardMemberModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}