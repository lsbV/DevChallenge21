namespace Core.Exceptions;

public class UnprocessableEntityException(string message) : Exception(message);