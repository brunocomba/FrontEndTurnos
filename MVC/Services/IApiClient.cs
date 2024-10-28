namespace MVC.ApiService
{
    public interface IApiClient<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string endpoint);
        Task<T> GetByIdAsync(string endpoint, int id);
        Task<string> CreateAsync(string endpoint, T entity);
        Task<bool> UpdateAsync(string endpoint, int id, T entity);
        Task<bool> DeleteAsync(string endpoint, int id);
    }
}
