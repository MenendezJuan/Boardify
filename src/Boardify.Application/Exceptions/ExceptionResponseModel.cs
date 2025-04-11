namespace Boardify.Application.Exceptions
{
    public class ExceptionResponseModel
    {
        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public string? StackTrace { get; set; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }
    }
}