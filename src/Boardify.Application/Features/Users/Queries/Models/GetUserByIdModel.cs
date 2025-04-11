using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Users.Queries.Models
{
    public class GetUserByIdModel
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public StatusEnum Status { get; set; }
        public bool IsAdmin { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
    }
}