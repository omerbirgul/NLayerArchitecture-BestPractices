using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.ExceptionHandler;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // business logic

        if (exception is CriticalException)
        {
            Console.WriteLine("Critical Erros info sended via e-mail");
        }

        return ValueTask.FromResult(false);
    }
}