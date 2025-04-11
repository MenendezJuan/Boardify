using Boardify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Application.Interfaces
{
    public interface IDatabaseService
    {
        public DbSet<User> Users { get; set; }
    }
}