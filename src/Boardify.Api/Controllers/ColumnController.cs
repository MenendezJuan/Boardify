using Boardify.Api.Controllers.Base;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Api.Controllers
{
    public class ColumnController : TrelloControllerBase
    {
        private readonly IColumnBusinessLogic _columnBusinessLogic;

        public ColumnController(IColumnBusinessLogic columnBusinessLogic)
        {
            _columnBusinessLogic = columnBusinessLogic ?? throw new ArgumentNullException(nameof(columnBusinessLogic));
        }

        /// <summary>
        /// Updates the order of columns.
        /// </summary>
        /// <param name="model">The model containing the new column order.</param>
        /// <returns>The updated list of columns.</returns>
        [HttpPut("update-column-order")]
        public async Task<IActionResult> UpdateColumnOrder([FromBody] UpdateColumnOrderModel model)
        {
            var result = await _columnBusinessLogic.UpdateColumnOrder(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Creates a new Column.
        /// </summary>
        /// <param name="columnModel">The column model.</param>
        /// <returns>The created column.</returns>
        [HttpPost("create-column")]
        public async Task<IActionResult> Create([FromBody] CreateColumnModel columnModel)
        {
            var result = await _columnBusinessLogic.CreateColumnAsync(columnModel);
            return result.IsSuccess() ? Created("", result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an existing Column.
        /// </summary>
        /// <param name="model">The update column model.</param>
        /// <returns>The updated column.</returns>
        [HttpPut("update-column")]
        public async Task<IActionResult> UpdateColumn([FromBody] UpdateColumnModel model)
        {
            var result = await _columnBusinessLogic.UpdateColumnAsync(model);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a Column by its ID.
        /// </summary>
        /// <param name="id">The ID of the column to delete.</param>
        /// <returns>NoContent if successful, BadRequest if the deletion fails.</returns>
        [HttpDelete("delete-column/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _columnBusinessLogic.DeleteColumnAsync(id);
            return result.IsSuccess() ? NoContent() : NotFound(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Retrieves all Columns.
        /// </summary>
        /// <returns>A list of all columns.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllColumns()
        {
            var result = await _columnBusinessLogic.GetAllColumnsAsync();
            return result.IsSuccess() ? Ok(result.SuccessValue) : NotFound(new { result.Errors, result.Message });
        }
    }
}