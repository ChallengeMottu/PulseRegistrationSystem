namespace PulseRegistrationSystem.Infraestructure.Repositories.Interface;

public interface IMethodsRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}