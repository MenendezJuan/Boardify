namespace Boardify.Application.Interfaces.Specific.Users
{
    public interface IGetImageQuery
    {
        Task<string> GetImageAsync(string relativeUrl);
    }
}