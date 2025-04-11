using Boardify.Application.Interfaces.Specific.Files;
using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Features.Shared
{
    public class FileService : IFileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<string> SaveAttachmentAsync(IFormFile file, string cardId)
        {
            string allowedExtensions = ".png;.jpg;.gif;.jpeg;.pdf;.doc;.docx;.txt";
            string extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Unsupported file format. Allowed extensions: " + allowedExtensions);
            }

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments", cardId);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var request = context.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/attachments/{cardId}/{fileName}";
        }

        public string GetAttachmentPath(string relativeUrl)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                return filePath;
            }

            throw new FileNotFoundException("The specified file was not found.", filePath);
        }

        public void DeleteAttachment(string cardId, string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments", cardId, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string SaveAvatarBase64(string base64Image, string userId, string extension)
        {
            string allowedExtensions = ".png;.jpg;.gif;.jpeg";
            if (!allowedExtensions.Contains("." + extension.ToLower()))
            {
                throw new ArgumentException("Unsupported image format. Allowed extensions: " + allowedExtensions);
            }
            string imageName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + extension;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ClearDirectory(new DirectoryInfo(path));

            string base64WithoutHeader = base64Image.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(base64WithoutHeader);

            string filePath = Path.Combine(path, imageName);
            File.WriteAllBytes(filePath, imageBytes);

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            var request = context.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/uploads/{userId}/{imageName}";
        }

        public string GetImagePath(string relativeUrl)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                return filePath;
            }

            throw new FileNotFoundException("The specified file was not found.", filePath);
        }

        public void DeleteAvatar(string userId)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId);

            if (Directory.Exists(path))
            {
                ClearDirectory(new DirectoryInfo(path));
                Directory.Delete(path);
            }
        }

        private void ClearDirectory(DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                subDirectory.Delete(true);
            }
        }
    }
}