using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Interfaces.Specific.Files
{
    public interface IFileService
    {
        Task<string> SaveAttachmentAsync(IFormFile file, string cardId);

        string GetAttachmentPath(string relativeUrl);

        void DeleteAttachment(string cardId, string fileName);

        string SaveAvatarBase64(string base64Image, string userId, string extension);

        void DeleteAvatar(string userId);

        string GetImagePath(string relativeUrl);
    }
}