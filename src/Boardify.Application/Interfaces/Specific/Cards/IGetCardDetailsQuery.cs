﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Cards.Models;

namespace Boardify.Application.Interfaces.Specific.Cards
{
    public interface IGetCardDetailsQuery
    {
        Task<IResult<BoardCardDetailResponse>> GetBoardCardAsync(int cardId);
    }
}