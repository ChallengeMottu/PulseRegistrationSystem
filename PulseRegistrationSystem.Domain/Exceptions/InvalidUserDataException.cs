namespace PulseRegistrationSystem.Domain.Exceptions;

public class InvalidUserDataException : Exception
{
    public InvalidUserDataException() : base("Dados do usuário inválidos.") { }
 
    public InvalidUserDataException(string message) : base(message) { }
 
    public InvalidUserDataException(string message, Exception innerException)
        : base(message, innerException) { }
}