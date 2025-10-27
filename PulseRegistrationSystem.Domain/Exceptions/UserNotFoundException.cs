namespace PulseRegistrationSystem.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string id)
        : base($"Usuário com id {id} não encontrado") {}
}