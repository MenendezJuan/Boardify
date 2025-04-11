﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Columns.Models;

namespace Boardify.Application.Interfaces.Specific.Columns
{
    public interface IUpdateColumnOrderCommand
    {
        Task<IResult<List<ColumnResponseModel>>> UpdateColumnOrder(UpdateColumnOrderModel model);
    }
}