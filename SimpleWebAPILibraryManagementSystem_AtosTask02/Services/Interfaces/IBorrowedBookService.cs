using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces
{
    public interface IBorrowedBookService
    {
        Task<ApiResponse<bool>> CreateBorrowingAsync(int bookId, int borrowerId, BorrowedBookDto borrowedBookModel);

        Task<List<DisplayBorrowedBook>> GetBorrowedBooksAsync();
        Task<ApiResponse<List<DisplayBorrowedBook>>> GetBorrowersByBookIdAsync(int bookId);
        Task<ApiResponse<List<DisplayBorrowedBook>>> GetBorrowedBookByBorrowerIdAsync(int borrowerId);
        Task<ApiResponse<bool>> DeleteBorrowedBookAsync(int bookId,int borrowerId);

    }
}
