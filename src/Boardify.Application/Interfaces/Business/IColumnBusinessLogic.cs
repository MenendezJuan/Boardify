﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Columns.Models;

namespace Boardify.Application.Interfaces.Business
{
    public interface IColumnBusinessLogic
    {
        Task<IResult<CreateColumnResponseModel>> CreateColumnAsync(CreateColumnModel model);

        Task<IResult<UpdateColumnResponseModel>> UpdateColumnAsync(UpdateColumnModel model);

        Task<IResult<bool>> DeleteColumnAsync(int id);

        Task<IResult<IEnumerable<ColumnResponseModel>>> GetAllColumnsAsync();

        Task<IResult<List<ColumnResponseModel>>> UpdateColumnOrder(UpdateColumnOrderModel model);
    }
}