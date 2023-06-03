using System.Net;

namespace UtilityLibrary
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public List<string>? ErrorMessages { get; set; }

        public void BuildResponse(HttpStatusCode statusCode,
            object? result = null,
            List<string>? errors = null,
            bool isSuccess = true)
        {
            StatusCode = statusCode;
            Result = result;
            IsSuccess = isSuccess;
            ErrorMessages = errors;
        }
    }
}
