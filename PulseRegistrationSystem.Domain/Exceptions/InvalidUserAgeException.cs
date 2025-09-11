namespace PulseRegistrationSystem.Domain.Exceptions;

public class InvalidUserAgeException : Exception
{
    public InvalidUserAgeException() : base("O colaborador deve ser maior de idade") { }
    public InvalidUserAgeException(string message) : base(message){}
}