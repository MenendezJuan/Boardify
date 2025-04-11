using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Cedeira.Essentials.NET.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Business;
using Boardify.Application.Features.Activity.Command;
using Boardify.Application.Features.Attachments.Commands;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.Attachments.Validators;
using Boardify.Application.Features.Boards.Commands;
using Boardify.Application.Features.Boards.Commands.CommandValidators;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Queries;
using Boardify.Application.Features.CardLabels.Commands;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Features.CardLabels.Queries;
using Boardify.Application.Features.Cards.Commands;
using Boardify.Application.Features.Cards.Commands.CommandValidators;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Features.Cards.Queries;
using Boardify.Application.Features.ChecklistItems.Commands;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Features.ChecklistItems.Validators;
using Boardify.Application.Features.Columns.Commands;
using Boardify.Application.Features.Columns.Commands.CommandValidators;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Features.Columns.Queries;
using Boardify.Application.Features.Comments.Commands;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Features.Shared;
using Boardify.Application.Features.Users.Commands;
using Boardify.Application.Features.Users.Commands.CommandValidators;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Users.Queries.GetAvatar;
using Boardify.Application.Features.Users.Queries.GetById.Users;
using Boardify.Application.Features.Users.Queries.GetUsersByEmailAddress;
using Boardify.Application.Features.Users.Queries.Login;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Features.Users.Queries.QueryValidators;
using Boardify.Application.Features.Users.Queries.SearchUsers;
using Boardify.Application.Features.Users.Queries.TokenRevalidation;
using Boardify.Application.Features.Workspaces.Commands;
using Boardify.Application.Features.Workspaces.Commands.CommandValidators;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Features.Workspaces.Queries;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Attachment;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Application.Interfaces.Specific.Files;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Application.MappingProfiles;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Boardify.Application
{
    public static class DependencyInjectionServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);

            #region InjectValidators
            //User validators
            services.AddTransient<IValidator<CreateUserModel>, CreateUserCommandValidator>();
            services.AddTransient<IValidator<UpdateUserModel>, UpdateUserCommandValidator>();
            services.AddTransient<IValidator<GetUserByEmailAddressModel>, GetUserByEmailAddressModelValidator>();
            services.AddTransient<IValidator<LoginModel>, LoginValidator>();
            services.AddTransient<IValidator<UpdatePasswordModel>, UpdatePasswordModelValidator>();

            //Command validators
            services.AddTransient<IValidator<AddMemberToBoardModel>, AddMemberToBoardCommandValidator>();
            services.AddTransient<IValidator<CreateBoardModel>, CreateBoardModelValidator>();
            services.AddTransient<IValidator<RemoveMemberFromBoardModel>, RemoveMemberFromBoardCommandValidator>();
            services.AddTransient<IValidator<UpdateBoardModel>, UpdateBoardModelValidator>();
            services.AddTransient<IValidator<AddLabelToBoardModel>, AddLabelToBoardCommandValidator>();
            services.AddTransient<IValidator<UpdateBoardLabelModel>, UpdateBoardLabelModelValidator>();
            services.AddTransient<IValidator<RemoveLabelFromBoardModel>, RemoveLabelFromBoardCommandValidator>();

            //Workspace Validators
            services.AddTransient<IValidator<CreateWorkspaceModel>, CreateWorkspaceCommandValidator>();
            services.AddTransient<IValidator<UpdateWorkspaceModel>, UpdateWorkspaceCommandValidator>();
            services.AddTransient<IValidator<AddMemberToWorkspaceModel>, AddMemberToWorkspaceModelValidator>();
            services.AddTransient<IValidator<RemoveMemberFromWorkspaceModel>, RemoveMemberFromWorkspaceModelValidator>();
            services.AddTransient<IValidator<UpdateWorkspaceModel>, UpdateWorkspaceCommandValidator>();

            //Column validators
            services.AddTransient<IValidator<CreateColumnModel>, CreateColumnModelValidator>();
            services.AddTransient<IValidator<UpdateColumnModel>, UpdateColumnValidator>();
            services.AddTransient<IValidator<UpdateColumnOrderModel>, UpdateColumnOrderModelValidator>();

            //Card validators
            services.AddTransient<IValidator<CreateCardModel>, CreateCardValidator>();
            services.AddTransient<IValidator<UpdateCardModel>, UpdateCardModelValidator>();
            services.AddTransient<IValidator<UpdateCardPriorityModel>, UpdateCardPriorityModelValidator>();
            services.AddTransient<IValidator<UpdateCardReporterModel>, UpdateCardReporterModelValidator>();
            services.AddTransient<IValidator<UpdateCardDatesModel>, UpdateCardDatesModelValidator>();
            services.AddTransient<IValidator<UpdateCardDescriptionModel>, UpdateCardDescriptionModelValidator>();
            services.AddTransient<IValidator<UpdateCardOrderModel>, UpdateCardOrderModelValidator>();
            services.AddTransient<IValidator<MoveCardRequestModel>, MoveCardRequestModelValidator>();
            services.AddTransient<IValidator<AddMemberRequestModel>, AddMemberRequestModelValidator>();
            services.AddTransient<IValidator<UpdateAttachmentRequestModel>, UpdateAttachmentRequestModelValidator>();
            services.AddTransient<IValidator<CardAttachment>, AddAttachmentRequestModelValidator>();
            services.AddTransient<IValidator<CreateChecklistItemRequest>, CreateChecklistItemRequestValidator>();
            services.AddTransient<IValidator<UpdateChecklistItemRequest>, UpdateChecklistItemRequestValidator>();
            services.AddTransient<IValidator<CardAttachment>, AddAttachmentRequestModelValidator>();


            #endregion InjectValidators

            #region UserServices

            services.AddTransient<ICreateCommand<CreateUserModel, CreateUserResponseModel>, CreateUserCommand>();
            services.AddTransient<IUpdateUserCommand<UpdateUserModel, UpdateUserResponseModel>, UpdateUserCommand>();
            services.AddTransient<IDeleteCommand<User>, DeleteUserCommand>();
            services.AddTransient<IGetByIdQuery<GetUserByIdModel, int>, GetUserByIdQuery>();
            services.AddTransient<IGetUserByEmailQuery, GetUserByEmailAddress>();
            services.AddTransient<IUpdatePasswordCommand, UpdateUserPasswordCommand>();
            services.AddTransient<ILoginQuery, LoginQuery>();
            services.AddTransient<IGetTokenRevalidationQuery, TokenRevalidationQuery>();

            #endregion UserServices

            #region WorkspaceServices

            services.AddTransient<ICreateWorkspaceCommand, CreateWorkspaceCommand>();
            services.AddTransient<IUpdateWorkspaceCommand<UpdateWorkspaceModel, UpdateWorkspaceResponseModel>, UpdateWorkspaceCommand>();
            services.AddTransient<IGetOwnedWorkspaceQuery, GetOwnedWorkspacesQuery>();

            #endregion WorkspaceServices

            #region UserWorkspaceServices

            services.AddTransient<IAddMemberToWorkspaceCommand, AddMemberToWorkspaceCommand>();

            services.AddTransient<IRemoveUserWorkspaceCommand, RemoveMemberFromWorkspaceCommand>();

            services.AddTransient<IGetWorkspaceMembersQuery, GetWorkspaceMembersQuery>();

            services.AddTransient<IGetUserWorkspacesQuery, GetUserWorkspacesQuery>();

            services.AddTransient<ISearchUsersQuery, SearchUsersQuery>();
            services.AddTransient<IGetImageQuery, GetImageQuery>();

            #endregion UserWorkspaceServices

            #region BoardServices

            services.AddTransient<ICreateBoardCommand<CreateBoardModel, CreateBoardResponseModel>, CreateBoardCommand>();
            services.AddTransient<IUpdateBoardCommand<UpdateBoardModel, UpdateBoardResponseModel>, UpdateBoardCommand>();
            services.AddTransient<IDeleteBoardCommand, DeleteBoardCommand>();
            services.AddTransient<IPermissionCheckQuery, PermissionCheckQuery>();
            services.AddTransient<IAddMemberToBoardCommand, AddMemberToBoardCommand>();
            services.AddTransient<IRemoveMemberFromBoardCommand, RemoveMemberFromBoardCommand>();
            services.AddTransient<IGetBoardMembersQuery, GetBoardMembersQuery>();
            services.AddTransient<IGetUserBoardsByWorkspaceQuery, GetUserBoardsByWorkspaceQuery>();
            services.AddTransient<IAddLabelToBoardCommand, AddLabelToBoardCommand>();
            services.AddTransient<IUpdateBoardLabelCommand<UpdateBoardLabelModel, UpdateBoardLabelResponseModel>, UpdateBoardLabelCommand>();
            services.AddTransient<IRemoveLabelFromBoardCommand, RemoveLabelFromBoardCommand>();
            services.AddTransient<IGetBoardLabelsQuery, GetBoardLabelsQuery>();
            services.AddTransient<IGetBoardWithColumnsAndCardsQuery, GetBoardWithColumnsAndCardsQuery>();

            #endregion BoardServices

            #region ColumnServices

            services.AddTransient<ICreateCommand<CreateColumnModel, CreateColumnResponseModel>, CreateColumnCommand>();
            services.AddTransient<IUpdateCommand<UpdateColumnModel, UpdateColumnResponseModel>, UpdateColumnCommand>();
            services.AddTransient<IDeleteCommand<Column>, DeleteColumnCommand>();
            services.AddTransient<IGetAllQuery<ColumnResponseModel>, GetAllColumnsQuery>();
            services.AddTransient<IUpdateColumnOrderCommand, UpdateColumnOrderCommand>();

            #endregion ColumnServices

            #region CardServices

            services.AddTransient<ICreateCommand<CreateCardModel, CreateCardResponseModel>, CreateCardCommand>();
            services.AddTransient<IUpdateCommand<UpdateCardModel, UpdateCardResponseModel>, UpdateCardCommand>();
            services.AddTransient<IDeleteCommand<Card>, DeleteCardCommand>();
            services.AddTransient<IGetAllQuery<CardResponseModel>, GetAllCardsQuery>();
            services.AddTransient<IUpdateCardOrderCommand, UpdateCardOrderCommand>();
            services.AddTransient<IMoveCardCommand, MoveCardCommand>();
            services.AddTransient<ICreateCommand<AddMemberRequestModel, CardMemberResponseModel>, AddMemberCommand>();
            services.AddTransient<IRemoveCardMemberCommand, RemoveMemberCommand>();
            services.AddTransient<IAddAttachmentCommand, AddAttachmentCommand>();
            services.AddTransient<IDeleteCommand<CardAttachment>, DeleteAttachmentCommand>();
            services.AddTransient<IUpdateCommand<UpdateAttachmentRequestModel, UpdateAttachmentResponseModel>, UpdateAttachmentCommand>();
            services.AddTransient<ICreateCommand<CreateChecklistItemRequest, ChecklistItemResponse>, CreateChecklistItemCommand>();
            services.AddTransient<IDeleteCommand<ChecklistItem>, DeleteChecklistItemCommand>();
            services.AddTransient<IUpdateCommand<UpdateChecklistItemRequest, UpdateChecklistItemResponse>, UpdateChecklistItemCommand>();
            services.AddTransient<ICreateCommand<CreateCardLabelModel, CardLabelModel>, CreateCardLabelCommand>();
            services.AddTransient<IDeleteCardLabelCommand, DeleteCardLabelCommand>();
            services.AddTransient<IUpdateCommand<UpdateCardLabelModel, CardLabelModel>, UpdateCardLabelCommand>();
            services.AddTransient<IGetAllQuery<CardLabelModel>, GetAllCardLabelsQuery>();
            services.AddTransient<IGetCardDetailsQuery, GetCardDetailsQuery>();
            services.AddTransient<IGetAssignableUsersQuery, GetAssignableUsersQuery>();
            services.AddTransient<IUpdateCommand<UpdateCardDatesModel, UpdateCardDatesResponseModel>, UpdateCardDatesCommand>();
            services.AddTransient<IUpdateCommand<UpdateCardPriorityModel, UpdateCardPriorityResponseModel>, UpdateCardPriorityCommand>();
            services.AddTransient<IUpdateCommand<UpdateCardDescriptionModel, UpdateCardDescriptionResponseModel>, UpdateCardDescriptionCommand>();
            services.AddTransient<IUpdateCommand<UpdateCardReporterModel, UpdateCardReporterResponseModel>, UpdateCardReporterCommand>();
            services.AddTransient<ICreateCommand<AddCommentModel, AddCommentResponseModel>, AddCommentCommand>();
            services.AddTransient<IUpdateCommand<UpdateCommentModel, UpdateCommentResponseModel>, UpdateCommentCommand>();
            services.AddTransient<IDeleteCommand<CardComment>, DeleteCommentCommand>();
            services.AddTransient<IRecordCardActivity, RecordCardActivity>();
            #endregion CardServices

            #region businessLogic

            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
            services.AddScoped<IWorkspaceBusinessLogic, WorkspaceBusinessLogic>();
            services.AddScoped<IBoardBusinessLogic, BoardBusinessLogic>();
            services.AddScoped<IColumnBusinessLogic, ColumnBusinesLogic>();
            services.AddScoped<ICardBusinessLogic, CardBusinessLogic>();

            #endregion businessLogic

            #region OtherServices

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IResultFactory, ResultFactory>();

            #endregion OtherServices

            return services;
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new UserProfile());
                config.AddProfile(new WorkspaceProfile());
                config.AddProfile(new UserWorkspaceProfile());
                config.AddProfile(new BoardProfile());
                config.AddProfile(new BoardMemberProfile());
                config.AddProfile(new ColumnProfile());
                config.AddProfile(new CardProfile());
                config.AddProfile(new CardMemberProfile());
                config.AddProfile(new AttachmentProfile());
                config.AddProfile(new ChecklistItemProfile());
                config.AddProfile(new CardLabelProfile());
                config.AddProfile(new CommentProfile());
            });
            services.AddSingleton(mapper.CreateMapper());
        }
    }
}