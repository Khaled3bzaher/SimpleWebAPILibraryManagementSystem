
namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> SaveChanges();
        IQueryable<T> GetAsQueryAble();
        Task<bool> isExistById(int id);
        Task<bool> isExistByName(string Name);
    }
}
