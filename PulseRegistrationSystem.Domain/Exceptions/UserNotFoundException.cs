namespace PulseRegistrationSystem.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id)
        : base($"Usuário com id {id} não encontrado") {}
}