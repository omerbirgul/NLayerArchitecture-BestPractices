namespace Services.ExceptionHandlers;

public class CriticalException(string Message) : Exception(Message);
