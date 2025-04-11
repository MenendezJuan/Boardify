using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;
using Boardify.Persistence.Extensions;
using Boardify.Persistence.Repositories;
using Boardify.Persistence.Repositories.Specifics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardify.Persistence
{
    public static class DependencyInjectionServices
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextMySqlServer(configuration);
            AddUserServices(services);
            AddWorkspaceServices(services);
            AddBoardServices(services);
            AddColumnServices(services);
            AddCardServices(services);
            AddAttachmentServices(services);
            AddChecklistItemServices(services);
            AddCardLabelServices(services);
            AddCardCommentServices(services);
            AddCardActivityServices(services);
            services.AddServices();
            return services;
        }

        private static void AddUserServices(IServiceCollection services)
        {
            services.AddScoped<IUserQueryRepository, UserRepository>();
            services.AddScoped<ICommandRepository<User>, CommandRepository<User>>();
            services.AddScoped<IQueryRepository<User>, QueryRepository<User>>();
            services.AddScoped<IUserWorkspaceRepository, UserWorkspaceRepository>();
        }

        private static void AddWorkspaceServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<Workspace>, CommandRepository<Workspace>>();
            services.AddScoped<IQueryRepository<Workspace>, QueryRepository<Workspace>>();
            services.AddScoped<ICommandRepository<UserWorkspace>, CommandRepository<UserWorkspace>>();
            services.AddScoped<IQueryRepository<UserWorkspace>, QueryRepository<UserWorkspace>>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        }

        private static void AddBoardServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<Board>, CommandRepository<Board>>();
            services.AddScoped<IQueryRepository<Board>, QueryRepository<Board>>();
            services.AddScoped<IBoardMemberRepository, BoardMemberRepository>();
            services.AddScoped<ICommandRepository<BoardMember>, CommandRepository<BoardMember>>();
            services.AddScoped<IBoardRepository, BoardRepository>();
        }

        private static void AddColumnServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<Column>, CommandRepository<Column>>();
            services.AddScoped<IQueryRepository<Column>, QueryRepository<Column>>();
            services.AddScoped<ICommandRepository<BoardLabel>, CommandRepository<BoardLabel>>();
            services.AddScoped<IQueryRepository<BoardLabel>, QueryRepository<BoardLabel>>();
            services.AddScoped<IBoardLabelRepository, BoardLabelRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
        }

        private static void AddCardServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<Card>, CommandRepository<Card>>();
            services.AddScoped<IQueryRepository<Card>, QueryRepository<Card>>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICardMemberRepository, CardMemberRepository>();
            services.AddScoped<ICommandRepository<CardMember>, CommandRepository<CardMember>>();
            services.AddScoped<IQueryRepository<CardMember>, QueryRepository<CardMember>>();
        }

        private static void AddAttachmentServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<CardAttachment>, CommandRepository<CardAttachment>>();
            services.AddScoped<IQueryRepository<CardAttachment>, QueryRepository<CardAttachment>>();
        }

        private static void AddChecklistItemServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<ChecklistItem>, CommandRepository<ChecklistItem>>();
            services.AddScoped<IQueryRepository<ChecklistItem>, QueryRepository<ChecklistItem>>();
        }

        private static void AddCardLabelServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<CardLabel>, CommandRepository<CardLabel>>();
            services.AddScoped<IQueryRepository<CardLabel>, QueryRepository<CardLabel>>();
            services.AddScoped<ICardLabelRepository, CardLabelRepository>();
        }

        private static void AddCardCommentServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<CardComment>, CommandRepository<CardComment>>();
            services.AddScoped<IQueryRepository<CardComment>, QueryRepository<CardComment>>();  
        }

        private static void AddCardActivityServices(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository<CardActivity>, CommandRepository<CardActivity>>();
            services.AddScoped<IQueryRepository<CardActivity>, QueryRepository<CardActivity>>();
        }
    }
}