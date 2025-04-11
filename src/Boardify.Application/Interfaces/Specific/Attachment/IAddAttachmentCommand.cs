﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Attachments.Models;
using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Interfaces.Specific.Attachment
{
    public interface IAddAttachmentCommand
    {
        Task<IResult<AttachmentResponseModel>> AddAttachmentAsync(IFormFile file, int cardId);
    }
}