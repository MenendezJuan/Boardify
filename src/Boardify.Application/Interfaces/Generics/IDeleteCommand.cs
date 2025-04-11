using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Generics
{
    public interface IDeleteCommand<T>
    {
        Task<IResult<bool>> Delete(int id);
    }
}