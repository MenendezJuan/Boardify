using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Interfaces.Specific.Files;
using Boardify.Application.Interfaces.Specific.Users;

namespace Boardify.Application.Features.Users.Queries.GetAvatar
{
    public class GetImageQuery : IGetImageQuery
    {
        private readonly IFileService _fileService;

        public GetImageQuery(IFileService fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<string> GetImageAsync(string relativeUrl)
        {
            var imagePath = _fileService.GetImagePath(relativeUrl);

            if (imagePath == null)
            {
                throw new NotFoundException("Imagen no encontrada");
            }

            return await Task.FromResult(imagePath);
        }
    }
}