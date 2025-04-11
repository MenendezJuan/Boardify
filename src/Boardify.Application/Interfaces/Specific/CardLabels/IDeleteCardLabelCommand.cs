using Boardify_Essentials.Extensions.System.ResultPattern;

namespace Boardify.Application.Interfaces.Specific.CardLabels
{
    public interface IDeleteCardLabelCommand
    {
        Task<IResult<bool>> Delete(int id, int idt);
    }
}