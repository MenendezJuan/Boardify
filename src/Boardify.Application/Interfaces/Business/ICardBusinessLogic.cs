﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Features.Users.Models;
using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Interfaces.Business
{
    public interface ICardBusinessLogic
    {
        Task<IResult<CreateCardResponseModel>> CreateCardAsync(CreateCardModel model);

        Task<IResult<UpdateCardResponseModel>> UpdateCardAsync(UpdateCardModel model);

        Task<IResult<bool>> DeleteCardAsync(int id);

        Task<IResult<IEnumerable<CardResponseModel>>> GetAllCardsAsync();

        Task<IResult<List<UpdateCardOrderResponseModel>>> UpdateCardOrder(UpdateCardOrderModel model);


        Task<IResult<CardResponseModel>> MoveCard(MoveCardRequestModel model);

        Task<IResult<CardMemberResponseModel>> AddMember(AddMemberRequestModel model);

        Task<IResult<bool>> RemoveMember(int memberId, int cardId);

        Task<IResult<AttachmentResponseModel>> AddAttachmentAsync(IFormFile file, int cardId);

        Task<IResult<bool>> DeleteAttachmentAsync(int attachmentId);

        Task<IResult<UpdateAttachmentResponseModel>> UpdateAttachmentAsync(UpdateAttachmentRequestModel model);

        Task<IResult<ChecklistItemResponse>> CreateChecklistItem(CreateChecklistItemRequest model);

        Task<IResult<bool>> DeleteChecklistItemAsync(int checklistItemId);

        Task<IResult<UpdateChecklistItemResponse>> UpdateChecklistItem(UpdateChecklistItemRequest request);

        Task<IResult<CardLabelModel>> CreateCardLabel(CreateCardLabelModel model);

        Task<IResult<bool>> DeleteCardLabel(int cardId, int labelId);

        Task<IResult<CardLabelModel>> UpdateCardLabel(UpdateCardLabelModel updateCardLabelModel);

        Task<IResult<IEnumerable<CardLabelModel>>> GetAllCardLabelsAsync();

        Task<IResult<BoardCardDetailResponse>> GetBoardCardDetailAsync(int cardId);

        Task<IResult<List<UserResponseModel>>> GetAssignableUsers(int cardId);
        Task<IResult<UpdateCardDatesResponseModel>> UpdateCardDates(UpdateCardDatesModel request);
        Task<IResult<UpdateCardPriorityResponseModel>> UpdateCardPriority(UpdateCardPriorityModel request);
        Task<IResult<UpdateCardDescriptionResponseModel>> UpdateCardDescription(UpdateCardDescriptionModel request);
        Task<IResult<UpdateCardReporterResponseModel>> UpdateCardReporter(UpdateCardReporterModel request);
        Task<IResult<AddCommentResponseModel>> AddCommentToCard(AddCommentModel addCommentModel);
        Task<IResult<UpdateCommentResponseModel>> UpdateComment(UpdateCommentModel updateCommentModel);
        Task<IResult<bool>> DeleteComment(int commentId);
    }
}