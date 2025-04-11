﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;

namespace Boardify.Application.Interfaces.Specific.Boards
{
    public interface IGetBoardWithColumnsAndCardsQuery
    {
        Task<IResult<BoardResponseDto>> GetBoardColumnsAndCards(int boardId);
    }
}