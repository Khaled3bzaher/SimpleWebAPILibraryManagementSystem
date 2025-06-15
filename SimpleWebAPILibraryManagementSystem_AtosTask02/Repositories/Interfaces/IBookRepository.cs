using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<DisplayBookDto>> GetDisplayBooks();
        Task<DisplayBookDto?> GetDisplayBookById(int id);
        Task<List<DisplayBookDto>> GetAllAuthorBooks(int authorId);
        Task<List<DisplayBookDto>> GetAllCategoryBooks(int categoryId);

    }
}
