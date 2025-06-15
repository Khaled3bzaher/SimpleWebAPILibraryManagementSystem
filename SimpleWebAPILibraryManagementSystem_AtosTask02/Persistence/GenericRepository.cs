

using Microsoft.EntityFrameworkCore;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Data;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChanges();


        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAsQueryAble()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async Task<bool> isExistById(int id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<bool> isExistByName(string Name)
        {
            return await _dbSet.AnyAsync(e => EF.Property<string>(e, "Name") == Name.Trim().ToLower());

        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChanges();

        }
    }
}
