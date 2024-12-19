using System.Net;

namespace Services;

public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    public HttpStatusCode Status { get; set; }
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    public bool IsFail => !IsSuccess;

    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessage,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = [errorMessage],
            Status = statusCode
        };
    }
}