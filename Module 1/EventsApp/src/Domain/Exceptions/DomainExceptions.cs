namespace EventsApp.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message);

public class AlreadyExistsException(string message) : Exception(message);
