using Boardify.Api.Controllers.Base;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Api.Controllers
{
    public class CardController : TrelloControllerBase
    {
        private readonly ICardBusinessLogic _cardBusinessLogic;

        public CardController(ICardBusinessLogic cardBusinessLogic)
        {
            _cardBusinessLogic = cardBusinessLogic ?? throw new ArgumentNullException(nameof(cardBusinessLogic));
        }

        /// <summary>
        /// Updates the order of the cards.
        /// </summary>
        /// <param name="model">The model containing the updated order of the cards.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-cards-order")]
        public async Task<IActionResult> UpdateCardOrder([FromBody] UpdateCardOrderModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardOrder(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="cardModel">The model containing the details of the new card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCardModel cardModel)
        {
            var result = await _cardBusinessLogic.CreateCardAsync(cardModel);
            return result.IsSuccess() ? Created("", result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds members to a card.
        /// </summary>
        /// <param name="addMemberModel">The model containing the member details to be added to the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("add-card-members")]
        public async Task<IActionResult> AddMember([FromBody] AddMemberRequestModel addMemberModel)
        {
            var result = await _cardBusinessLogic.AddMember(addMemberModel);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an existing card.
        /// </summary>
        /// <param name="model">The model containing the updated details of the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCardModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardAsync(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Moves a card between columns.
        /// </summary>
        /// <param name="model">The model containing the details of the move.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("move-between-columns")]
        public async Task<IActionResult> MoveCard([FromBody] MoveCardRequestModel model)
        {
            var result = await _cardBusinessLogic.MoveCard(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <param name="id">The ID of the card to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cardBusinessLogic.DeleteCardAsync(id);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Removes a member from a card.
        /// </summary>
        /// <param name="memberId">The ID of the member to be removed.</param>
        /// <param name="cardId">The ID of the card from which the member will be removed.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("remove-card/{cardId}-member/{memberId}")]
        public async Task<IActionResult> RemoveMember(int memberId, int cardId)
        {
            var result = await _cardBusinessLogic.RemoveMember(memberId, cardId);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <returns>An IActionResult containing a list of all cards.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cardBusinessLogic.GetAllCardsAsync();
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds an attachment to a card.
        /// </summary>
        /// <param name="cardId">The ID of the card to which the attachment will be added.</param>
        /// <param name="file">The attachment file to be added.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("add-attachment")]
        public async Task<IActionResult> AddAttachment(int cardId, IFormFile file)
        {
            var result = await _cardBusinessLogic.AddAttachmentAsync(file, cardId);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes an attachment from a card.
        /// </summary>
        /// <param name="attachmentId">The ID of the attachment to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("delete-attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var result = await _cardBusinessLogic.DeleteAttachmentAsync(attachmentId);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an attachment.
        /// </summary>
        /// <param name="model">The model containing the updated details of the attachment.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-attachments")]
        public async Task<IActionResult> UpdateAttachment([FromBody] UpdateAttachmentRequestModel model)
        {
            var result = await _cardBusinessLogic.UpdateAttachmentAsync(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Creates a checklist item for a card.
        /// </summary>
        /// <param name="model">The model containing the details of the checklist item.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("create-checklistItem")]
        public async Task<IActionResult> CreateChecklistItem([FromBody] CreateChecklistItemRequest model)
        {
            var result = await _cardBusinessLogic.CreateChecklistItem(model);
            return result.IsSuccess() ? CreatedAtAction(nameof(CreateChecklistItem), result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a checklist item from a card.
        /// </summary>
        /// <param name="checklistItemId">The ID of the checklist item to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("delete-checklistItem/{checklistItemId}")]
        public async Task<IActionResult> DeleteChecklistItem(int checklistItemId)
        {
            var result = await _cardBusinessLogic.DeleteChecklistItemAsync(checklistItemId);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates a checklist item.
        /// </summary>
        /// <param name="request">The model containing the updated details of the checklist item.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-checklistItem")]
        public async Task<IActionResult> UpdateChecklistItem([FromBody] UpdateChecklistItemRequest request)
        {
            var result = await _cardBusinessLogic.UpdateChecklistItem(request);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Creates a card label.
        /// </summary>
        /// <param name="createCardLabel">The model containing the details of the card label.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("create-cardLabel")]
        public async Task<IActionResult> CreateCardLabel([FromBody] CreateCardLabelModel createCardLabel)
        {
            var result = await _cardBusinessLogic.CreateCardLabel(createCardLabel);
            return result.IsSuccess() ? CreatedAtAction(nameof(CreateCardLabel), new { id = createCardLabel.CardId }, result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a card label.
        /// </summary>
        /// <param name="cardId">The ID of the card from which the label will be deleted.</param>
        /// <param name="labelId">The ID of the label to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("delete-cardLabel/{cardId}/{labelId}")]
        public async Task<IActionResult> DeleteCardLabel(int cardId, int labelId)
        {
            var result = await _cardBusinessLogic.DeleteCardLabel(cardId, labelId);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates a card label.
        /// </summary>
        /// <param name="request">The model containing the updated details of the card label.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-cardLabel")]
        public async Task<IActionResult> UpdateCardLabel([FromBody] UpdateCardLabelModel request)
        {
            var result = await _cardBusinessLogic.UpdateCardLabel(request);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets all card labels.
        /// </summary>
        /// <returns>An IActionResult containing a list of all card labels.</returns>
        [HttpGet("get-all-cardLabels")]
        public async Task<IActionResult> GetAllCardLabels()
        {
            var result = await _cardBusinessLogic.GetAllCardLabelsAsync();
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the details of a specific card.
        /// </summary>
        /// <param name="id">The ID of the card.</param>
        /// <returns>An IActionResult containing the details of the card.</returns>
        [HttpGet("get-card/{id}/details")]
        public async Task<IActionResult> GetCardDetails(int id)
        {
            var result = await _cardBusinessLogic.GetBoardCardDetailAsync(id);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the assignees of a specific card.
        /// </summary>
        /// <param name="cardId">The ID of the card.</param>
        /// <returns>An IActionResult containing a list of assignees for the card.</returns>
        [HttpGet("get-card-assignees")]
        public async Task<IActionResult> GetCardAssignees(int cardId)
        {
            var result = await _cardBusinessLogic.GetAssignableUsers(cardId);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates the dates of a card.
        /// </summary>
        /// <param name="model">The model containing the updated dates of the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-card-dates")]
        public async Task<IActionResult> UpdateCardDates([FromBody] UpdateCardDatesModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardDates(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates the priority of a card.
        /// </summary>
        /// <param name="model">The model containing the updated priority of the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-card-priority")]
        public async Task<IActionResult> UpdateCardPriority([FromBody] UpdateCardPriorityModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardPriority(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates the description of a card.
        /// </summary>
        /// <param name="model">The model containing the updated description of the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-card-description")]
        public async Task<IActionResult> UpdateCardDescription([FromBody] UpdateCardDescriptionModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardDescription(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates the reporters of a card.
        /// </summary>
        /// <param name="model">The model containing the updated reporters of the card.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-card-reporters")]
        public async Task<IActionResult> UpdateCardReporter([FromBody] UpdateCardReporterModel model)
        {
            var result = await _cardBusinessLogic.UpdateCardReporter(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds a comment to a card.
        /// </summary>
        /// <param name="model">The model containing the details of the comment.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("add-comment")]
        public async Task<IActionResult> AddCommentToCard([FromBody] AddCommentModel model)
        {
            var result = await _cardBusinessLogic.AddCommentToCard(model);
            return result.IsSuccess() ? CreatedAtAction(nameof(AddCommentToCard), result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates a comment on a card.
        /// </summary>
        /// <param name="updateCommentModel">The model containing the updated details of the comment.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentModel updateCommentModel)
        {
            var result = await _cardBusinessLogic.UpdateComment(updateCommentModel);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a comment from a card.
        /// </summary>
        /// <param name="commentId">The ID of the comment to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("delete-card-comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _cardBusinessLogic.DeleteComment(commentId);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }
    }
}