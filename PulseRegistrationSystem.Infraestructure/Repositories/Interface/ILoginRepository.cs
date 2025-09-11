using PulseRegistrationSystem.Domain.Entities;

namespace PulseRegistrationSystem.Infraestructure.Repositories.Interface;

public interface ILoginRepository : IMethodsRepository<Login>
{
    Task<Login?> GetByCpfAsync(string cpf);
}