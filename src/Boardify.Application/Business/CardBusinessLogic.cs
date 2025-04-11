using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.Attachments.Validators;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Attachment;
using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Application.Interfaces.Specific.Users;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Business
{
    public class CardBusinessLogic : ICardBusinessLogic
    {
        private readonly ICreateCommand<CreateCardModel, CreateCardResponseModel> _createCardCommand;
        private readonly IValidator<CreateCardModel> _createCardValidator;
        private readonly IUpdateCommand<UpdateCardModel, UpdateCardResponseModel> _updateCardCommand;
        private readonly IValidator<UpdateCardModel> _updateCardValidator;
        private readonly IDeleteCommand<Card> _deleteCardCommand;
        private readonly IGetAllQuery<CardResponseModel> _getAllCardsQuery;
        private readonly IUpdateCardOrderCommand _updateCardOrderCommand;
        private readonly IMoveCardCommand _moveCardCommand;
        private readonly ICreateCommand<AddMemberRequestModel, CardMemberResponseModel> _addMemberCommand;
        private readonly IRemoveCardMemberCommand _removeCardMemberCommand;
        private readonly IAddAttachmentCommand _addAttachmentCommand;
        private readonly IDeleteCommand<CardAttachment> _deleteAttachmentCommand;
        private readonly IUpdateCommand<UpdateAttachmentRequestModel, UpdateAttachmentResponseModel> _updateAttachmentCommand;
        private readonly ICreateCommand<CreateChecklistItemRequest, ChecklistItemResponse> _createChecklistCommand;
        private readonly IDeleteCommand<ChecklistItem> _deleteChecklistCommand;
        private readonly IUpdateCommand<UpdateChecklistItemRequest, UpdateChecklistItemResponse> _updateChecklistItemCommand;
        private readonly ICreateCommand<CreateCardLabelModel, CardLabelModel> _createCardLabelCommand;
        private readonly IDeleteCardLabelCommand _deleteCardLabelCommand;
        private readonly IUpdateCommand<UpdateCardLabelModel, CardLabelModel> _updateCardLabelCommand;
        private readonly IGetAllQuery<CardLabelModel> _getAllCardLabelsQuery;
        private readonly IGetCardDetailsQuery _getCardDetailsQuery;
        private readonly IGetAssignableUsersQuery _getAssignableUsersQuery;
        private readonly IUpdateCommand<UpdateCardDatesModel, UpdateCardDatesResponseModel> _updateDatesCommand;
        private readonly IUpdateCommand<UpdateCardPriorityModel, UpdateCardPriorityResponseModel> _updatePriorityCommand;
        private readonly IUpdateCommand<UpdateCardDescriptionModel, UpdateCardDescriptionResponseModel> _updateDescriptionCommand;
        private readonly IUpdateCommand<UpdateCardReporterModel, UpdateCardReporterResponseModel> _updateCardReporterCommand;
        private readonly IValidator<UpdateCardReporterModel> _updateCardReporterValidator;
        private readonly IValidator<UpdateCardDatesModel> _updateCardDatesValidator;
        private readonly IValidator<UpdateCardDescriptionModel> _updateCardDescriptionValidator;
        private readonly IValidator<UpdateCardPriorityModel> _updateCardPriorityValidator;
        private readonly IValidator<UpdateCardOrderModel> _updateCardOrderValidator;
        private readonly IValidator<AddMemberRequestModel> _addMemberValidator;
        private readonly IValidator<MoveCardRequestModel> _moveCardValidator;
        private readonly IValidator<UpdateAttachmentRequestModel> _updateCardAttachmentValidator;
        private readonly IValidator<CreateChecklistItemRequest> _createChecklistItemValidator;
        private readonly IValidator<UpdateChecklistItemRequest> _updateChecklistItemValidator;
        private readonly IValidator<CardAttachment> _addAttachmentValidator;
        private readonly IResultFactory _resultFactory;
        private readonly ICreateCommand<AddCommentModel, AddCommentResponseModel> _addCommentCommand;
        private readonly IUpdateCommand<UpdateCommentModel, UpdateCommentResponseModel> _updateCommentCommand;
        private readonly IDeleteCommand<CardComment> _deleteCommentCommand;
        private readonly IRecordCardActivity _recordCardActivity;

        public CardBusinessLogic(ICreateCommand<CreateCardModel, CreateCardResponseModel> createCardCommand,
            IValidator<CreateCardModel> createCardValidator,
            IUpdateCommand<UpdateCardModel, UpdateCardResponseModel> updateCardCommand,
            IValidator<UpdateCardModel> updateCardValidator,
            IDeleteCommand<Card> deleteCardCommand,
            IGetAllQuery<CardResponseModel> getAllCardsQuery,
            IUpdateCardOrderCommand updateCardOrderCommand,
            IMoveCardCommand moveCardCommand,
            ICreateCommand<AddMemberRequestModel, CardMemberResponseModel> addMemberCommand,
            IRemoveCardMemberCommand removeCardMemberCommand,
            IAddAttachmentCommand addAttachmentCommand,
            IDeleteCommand<CardAttachment> deleteAttachmentCommand,
            IUpdateCommand<UpdateAttachmentRequestModel, UpdateAttachmentResponseModel> updateAttachmentCommand,
            ICreateCommand<CreateChecklistItemRequest, ChecklistItemResponse> createChecklistCommand,
            IDeleteCommand<ChecklistItem> deleteChecklistCommand,
            IUpdateCommand<UpdateChecklistItemRequest, UpdateChecklistItemResponse> updateChecklistItemCommand,
            ICreateCommand<CreateCardLabelModel, CardLabelModel> createCardLabelCommand,
            IDeleteCardLabelCommand deleteCardLabelCommand,
            IUpdateCommand<UpdateCardLabelModel, CardLabelModel> updateCardLabelCommand,
            IGetAllQuery<CardLabelModel> getAllCardLabelsQuery,
            IGetCardDetailsQuery getCardDetailsQuery,
            IGetAssignableUsersQuery getAssignableUsersQuery,
            IUpdateCommand<UpdateCardDatesModel, UpdateCardDatesResponseModel> updateDatesCommand,
            IUpdateCommand<UpdateCardPriorityModel, UpdateCardPriorityResponseModel> updatePriorityCommand,
            IUpdateCommand<UpdateCardDescriptionModel, UpdateCardDescriptionResponseModel> updateDescriptionCommand,
            IUpdateCommand<UpdateCardReporterModel, UpdateCardReporterResponseModel> updateCardReporterCommand,
            IValidator<UpdateCardReporterModel> updateCardReporterValidator,
            IValidator<UpdateCardDatesModel> updateCardDatesValidator,
            IValidator<UpdateCardDescriptionModel> updateCardDescriptionValidator,
            IValidator<UpdateCardPriorityModel> updateCardPriorityValidator,
            IValidator<UpdateCardOrderModel> updateCardOrderValidator,
            IValidator<AddMemberRequestModel> addMemberValidator,
            IValidator<MoveCardRequestModel> moveCardValidator,
            IValidator<UpdateAttachmentRequestModel> updateCardAttachmentValidator,
            IValidator<CreateChecklistItemRequest> createChecklistItemValidator,
            IValidator<UpdateChecklistItemRequest> updateChecklistItemValidator,
            IResultFactory resultFactory,
            IValidator<CardAttachment> addAttachmentValidator,
            ICreateCommand<AddCommentModel, AddCommentResponseModel> addCommentCommand,
            IUpdateCommand<UpdateCommentModel, UpdateCommentResponseModel> updateCommentCommand,
            IDeleteCommand<CardComment> deleteCommentCommand,
            IRecordCardActivity recordCardActivity)
        {
            _createCardCommand = createCardCommand ?? throw new ArgumentNullException(nameof(createCardCommand));
            _createCardValidator = createCardValidator ?? throw new ArgumentNullException(nameof(createCardValidator));
            _updateCardCommand = updateCardCommand ?? throw new ArgumentNullException(nameof(updateCardCommand));
            _updateCardValidator = updateCardValidator ?? throw new ArgumentNullException(nameof(updateCardValidator));
            _deleteCardCommand = deleteCardCommand ?? throw new ArgumentNullException(nameof(deleteCardCommand));
            _getAllCardsQuery = getAllCardsQuery ?? throw new ArgumentNullException(nameof(getAllCardsQuery));
            _updateCardOrderCommand = updateCardOrderCommand ?? throw new ArgumentNullException(nameof(updateCardOrderCommand));
            _moveCardCommand = moveCardCommand ?? throw new ArgumentNullException(nameof(moveCardCommand));
            _addMemberCommand = addMemberCommand ?? throw new ArgumentNullException(nameof(addMemberCommand));
            _removeCardMemberCommand = removeCardMemberCommand ?? throw new ArgumentNullException(nameof(removeCardMemberCommand));
            _addAttachmentCommand = addAttachmentCommand ?? throw new ArgumentNullException(nameof(addAttachmentCommand));
            _deleteAttachmentCommand = deleteAttachmentCommand ?? throw new ArgumentNullException(nameof(deleteAttachmentCommand));
            _updateAttachmentCommand = updateAttachmentCommand ?? throw new ArgumentNullException(nameof(updateAttachmentCommand));
            _createChecklistCommand = createChecklistCommand ?? throw new ArgumentNullException(nameof(createChecklistCommand));
            _deleteChecklistCommand = deleteChecklistCommand ?? throw new ArgumentNullException(nameof(deleteChecklistCommand));
            _updateChecklistItemCommand = updateChecklistItemCommand ?? throw new ArgumentNullException(nameof(updateChecklistItemCommand));
            _createCardLabelCommand = createCardLabelCommand ?? throw new ArgumentNullException(nameof(createCardLabelCommand));
            _deleteCardLabelCommand = deleteCardLabelCommand ?? throw new ArgumentNullException(nameof(deleteCardLabelCommand));
            _updateCardLabelCommand = updateCardLabelCommand ?? throw new ArgumentNullException(nameof(updateCardLabelCommand));
            _getAllCardLabelsQuery = getAllCardLabelsQuery ?? throw new ArgumentNullException(nameof(getAllCardLabelsQuery));
            _getCardDetailsQuery = getCardDetailsQuery ?? throw new ArgumentNullException(nameof(getCardDetailsQuery));
            _getAssignableUsersQuery = getAssignableUsersQuery ?? throw new ArgumentNullException(nameof(getAssignableUsersQuery));
            _updateDatesCommand = updateDatesCommand ?? throw new ArgumentNullException(nameof(updateDatesCommand));
            _updatePriorityCommand = updatePriorityCommand ?? throw new ArgumentNullException(nameof(updatePriorityCommand));
            _updateDescriptionCommand = updateDescriptionCommand ?? throw new ArgumentNullException(nameof(updateDescriptionCommand));
            _updateCardReporterCommand = updateCardReporterCommand ?? throw new ArgumentNullException(nameof(updateCardReporterCommand));
            _updateCardReporterValidator = updateCardReporterValidator ?? throw new ArgumentNullException(nameof(updateCardReporterValidator));
            _updateCardDatesValidator = updateCardDatesValidator ?? throw new ArgumentNullException(nameof(updateCardDatesValidator));
            _updateCardDescriptionValidator = updateCardDescriptionValidator ?? throw new ArgumentNullException(nameof(updateCardDescriptionValidator));
            _updateCardPriorityValidator = updateCardPriorityValidator ?? throw new ArgumentNullException(nameof(updateCardPriorityValidator));
            _updateCardOrderValidator = updateCardOrderValidator ?? throw new ArgumentNullException(nameof(updateCardOrderValidator));
            _addMemberValidator = addMemberValidator ?? throw new ArgumentNullException(nameof(addMemberValidator));
            _moveCardValidator = moveCardValidator ?? throw new ArgumentNullException(nameof(moveCardValidator));
            _updateCardAttachmentValidator = updateCardAttachmentValidator ?? throw new ArgumentNullException(nameof(updateCardAttachmentValidator));
            _createChecklistItemValidator = createChecklistItemValidator ?? throw new ArgumentNullException(nameof(createChecklistItemValidator));
            _updateChecklistItemValidator = updateChecklistItemValidator ?? throw new ArgumentNullException(nameof(updateChecklistItemValidator));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
            _addAttachmentValidator = addAttachmentValidator ?? throw new ArgumentNullException(nameof(addAttachmentValidator));
            _addCommentCommand = addCommentCommand ?? throw new ArgumentNullException(nameof(addCommentCommand));
            _updateCommentCommand = updateCommentCommand;
            _deleteCommentCommand = deleteCommentCommand;
            _recordCardActivity = recordCardActivity;
        }

        public async Task<IResult<bool>> DeleteComment(int commentId)
        {
            return await _deleteCommentCommand.Delete(commentId);
        }

        public async Task<IResult<UpdateCommentResponseModel>> UpdateComment(UpdateCommentModel updateCommentModel)
        {
            var result = await _updateCommentCommand.Update(updateCommentModel);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(updateCommentModel.Id, null, CardEventTypeEnum.UpdatedComment, $"Comment updated to card {updateCommentModel.Id}");
            }

            return result;
        }

        public async Task<IResult<AddCommentResponseModel>> AddCommentToCard(AddCommentModel addCommentModel)
        {
            var result = await _addCommentCommand.Create(addCommentModel);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(addCommentModel.CardId, addCommentModel.UserId, CardEventTypeEnum.AddedComment, $"Comment added to card {addCommentModel.CardId} by user {addCommentModel.UserId}");
            }

            return result;
        }

        public async Task<IResult<UpdateCardReporterResponseModel>> UpdateCardReporter(UpdateCardReporterModel request)
        {
            var validation = await _updateCardReporterValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<UpdateCardReporterResponseModel>(errorDictionary);
            }

            var result = await _updateCardReporterCommand.Update(request);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(request.Id, request.ReporterId, CardEventTypeEnum.AddedReporter, $"Reporter {request.ReporterId} updated for card {request.Id}");
            }

            return result;
        }


        public async Task<IResult<UpdateCardDescriptionResponseModel>> UpdateCardDescription(UpdateCardDescriptionModel request)
        {
            var validation = await _updateCardDescriptionValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<UpdateCardDescriptionResponseModel>(errorDictionary);
            }

            var result = await _updateDescriptionCommand.Update(request);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(request.Id, null, CardEventTypeEnum.DescriptionUpdated, $"Description updated for card {request.Id}");
            }

            return result;
        }

        public async Task<IResult<UpdateCardPriorityResponseModel>> UpdateCardPriority(UpdateCardPriorityModel request)
        {
            var validation = await _updateCardPriorityValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<UpdateCardPriorityResponseModel>(errors);
            }

            var result = await _updatePriorityCommand.Update(request);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(request.Id, null, CardEventTypeEnum.ModifiedPriority, $"Priority changed to {request.Priority} updated for card {request.Id}");
            }
            return result;
        }

        public async Task<IResult<UpdateCardDatesResponseModel>> UpdateCardDates(UpdateCardDatesModel request)
        {
            var validation = await _updateCardDatesValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<UpdateCardDatesResponseModel>(errors);
            }

            var result = await _updateDatesCommand.Update(request);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(request.Id, null, CardEventTypeEnum.DueDateChanged, $"Dates changed to StartaDate: {request.StartDate} & DueDate: {request.DueDate} for card {request.Id}");
            }

            return result;
        }

        public async Task<IResult<List<UserResponseModel>>> GetAssignableUsers(int cardId)
        {
            return await _getAssignableUsersQuery.GetAssignableUsers(cardId);
        }

        public async Task<IResult<BoardCardDetailResponse>> GetBoardCardDetailAsync(int cardId)
        {
            return await _getCardDetailsQuery.GetBoardCardAsync(cardId);
        }

        public async Task<IResult<IEnumerable<CardLabelModel>>> GetAllCardLabelsAsync()
        {
            return await _getAllCardLabelsQuery.GetAllAsync();
        }

        public async Task<IResult<CardLabelModel>> UpdateCardLabel(UpdateCardLabelModel updateCardLabelModel)
        {
            return await _updateCardLabelCommand.Update(updateCardLabelModel);
        }

        public async Task<IResult<bool>> DeleteCardLabel(int cardId, int labelId)
        {
            return await _deleteCardLabelCommand.Delete(cardId, labelId);
        }

        public async Task<IResult<CardLabelModel>> CreateCardLabel(CreateCardLabelModel model)
        {
            var result = await _createCardLabelCommand.Create(model);
            return result;
        }
        public async Task<IResult<ChecklistItemResponse>> CreateChecklistItem(CreateChecklistItemRequest model)
        {
            var validationResult = await _createChecklistItemValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<ChecklistItemResponse>(errors);
            }

            var result = await _createChecklistCommand.Create(model);
            return result;
        }

        public async Task<IResult<UpdateChecklistItemResponse>> UpdateChecklistItem(UpdateChecklistItemRequest request)
        {
            var validationResult = await _updateChecklistItemValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<UpdateChecklistItemResponse>(errors);
            }

            var result = await _updateChecklistItemCommand.Update(request);
            return result;
        }

        public async Task<IResult<bool>> DeleteChecklistItemAsync(int checklistItemId)
        {
            return await _deleteChecklistCommand.Delete(checklistItemId);
        }


        public async Task<IResult<UpdateAttachmentResponseModel>> UpdateAttachmentAsync(UpdateAttachmentRequestModel model)
        {
            var validationResult = await _updateCardAttachmentValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<UpdateAttachmentResponseModel>(errors);
            }

            var result = await _updateAttachmentCommand.Update(model);
            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(model.Id, null, CardEventTypeEnum.ModifiedAttachment, $"Attachment updated for card {model.Id}");
            }

            return result;
        }

        public async Task<IResult<AttachmentResponseModel>> AddAttachmentAsync(IFormFile file, int cardId)
        {
            var cardAttachment = new CardAttachment
            {
                FileName = file.FileName,
                CardId = cardId
            };

            var validator = new AddAttachmentRequestModelValidator();
            var validationResult = await validator.ValidateAsync(cardAttachment);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<AttachmentResponseModel>(errors);
            }

            var result = await _addAttachmentCommand.AddAttachmentAsync(file, cardId);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(cardId, null, CardEventTypeEnum.AddedAttachment, $"Attachment added to card {cardId}");
            }

            return result;
        }

        public async Task<IResult<bool>> DeleteAttachmentAsync(int attachmentId)
        {
            return await _deleteAttachmentCommand.Delete(attachmentId);
        }

        public async Task<IResult<bool>> RemoveMember(int memberId, int cardId)
        {
            var result = await _removeCardMemberCommand.Delete(memberId, cardId);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(cardId, memberId, CardEventTypeEnum.MemberRemoved, $"User {memberId} removed from card {cardId}");
            }

            return result;
        }

        public async Task<IResult<CardMemberResponseModel>> AddMember(AddMemberRequestModel model)
        {
            var validationResult = await _addMemberValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<CardMemberResponseModel>(errors);
            }

           var result = await _addMemberCommand.Create(model);

            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(model.CardId, model.UserId, CardEventTypeEnum.MemberAdded, $"User {model.UserId} added to card {model.CardId}");
            }

            return result;
        }


        public async Task<IResult<CardResponseModel>> MoveCard(MoveCardRequestModel model)
        {
            var validationResult = await _moveCardValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errorDictionary = validationResult.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<CardResponseModel>(errorDictionary);
            }

            var result = await _moveCardCommand.MoveCardAsync(model);
            if (result.IsSuccess())
            {
                await _recordCardActivity.LogActivityAsync(model.CardId, model.DestinationColumnId, CardEventTypeEnum.MemberAdded, $"Card moved from {model.CardId} to column {model.DestinationColumnId}");
            }

            return result;

        }

        public async Task<IResult<List<UpdateCardOrderResponseModel>>> UpdateCardOrder(UpdateCardOrderModel model)
        {
            var validation = await _updateCardOrderValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<List<UpdateCardOrderResponseModel>>(errorDictionary);
            }

            return await _updateCardOrderCommand.UpdateCardOrder(model);
        }


        public async Task<IResult<IEnumerable<CardResponseModel>>> GetAllCardsAsync()
        {
            return await _getAllCardsQuery.GetAllAsync();
        }

        public async Task<IResult<CreateCardResponseModel>> CreateCardAsync(CreateCardModel model)
        {
            var validationResult = await _createCardValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errorDictionary = validationResult.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<CreateCardResponseModel>(errorDictionary);
            }

            return await _createCardCommand.Create(model);
        }

    public async Task<IResult<UpdateCardResponseModel>> UpdateCardAsync(UpdateCardModel model)
        {
            var validation = await _updateCardValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<UpdateCardResponseModel>(errorDictionary);
            }

            return await _updateCardCommand.Update(model);
        }

        public async Task<IResult<bool>> DeleteCardAsync(int id)
        {
            return await _deleteCardCommand.Delete(id);
        }
    }
}