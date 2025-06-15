using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces
{
    public interface IBorrowedBookRepository : IGenericRepository<BorrowedBook>
    {
        Task<List<DisplayBorrowedBook>> GetDisplayBorrowedBooks();
        Task<List<DisplayBorrowedBook>> GetDisplayBorrowersByBookId(int bookId);
        Task<List<DisplayBorrowedBook>> GetDisplayBorrowedBookByBorrowerId(int borrowerId);
        Task<BorrowedBook?> GetBorrowedBook(int bookId, int borrowerId);

        Task<bool> isBorrowed(int book, int borrowerId);
    }
}
