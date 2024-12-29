using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Services.ExceptionHandlers;

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