using Boardify.Application.Interfaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Boardify.Persistence.Database
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityConfiguration(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<UserWorkspace> UserWorkspaces { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardMember> BoardsMember { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<BoardLabel> BoardLabels { get; set; }
        public DbSet<CardMember> CardMembers { get; set; }
        public DbSet<CardAttachment> CardAttachments { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<CardLabel> CardLabels { get; set; }
        public DbSet<CardComment> CardComments { get; set; }
        public DbSet<CardActivity> CardActivities { get; set; }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new WorkspaceConfiguration().Configure(modelBuilder.Entity<Workspace>());
            new UserWorkspaceConfiguration().Configure(modelBuilder.Entity<UserWorkspace>());
            new BoardConfiguration().Configure(modelBuilder.Entity<Board>());
            new BoardMemberConfiguration().Configure(modelBuilder.Entity<BoardMember>());
            new ColumnConfiguration().Configure(modelBuilder.Entity<Column>());
            new CardConfiguration().Configure(modelBuilder.Entity<Card>());
            new BoardLabelConfiguration().Configure(modelBuilder.Entity<BoardLabel>());
            new CardMemberConfiguration().Configure(modelBuilder.Entity<CardMember>());
            new CardAttachmentConfiguration().Configure(modelBuilder.Entity<CardAttachment>());
            new ChecklistItemConfiguration().Configure(modelBuilder.Entity<ChecklistItem>());
            new CardLabelConfiguration().Configure(modelBuilder.Entity<CardLabel>());
            new CommentConfiguration().Configure(modelBuilder.Entity<CardComment>());
            new CardActivityConfiguration().Configure(modelBuilder.Entity<CardActivity>());
        }
    }
}