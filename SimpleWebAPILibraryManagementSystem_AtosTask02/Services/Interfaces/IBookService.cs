using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces
{
    public interface IBookService
    {
        Task<ApiResponse<bool>> CreateBookAsync(int autherId, int categoryId, BookDto bookModel);

        Task<ApiResponse<bool>> EditBookAsync(int bookId,int autherId, int categoryId, BookDto bookModel);

        Task<List<DisplayBookDto>> GetBooksAsync();
        Task<ApiResponse<DisplayBookDto>> GetBookAsync(int bookId);
        Task<ApiResponse<List<DisplayBookDto>>> GetCategoryBooksAsync(int categoryId);
        Task<ApiResponse<List<DisplayBookDto>>> GetAuthorBooksAsync(int authorId);

        Task<ApiResponse<bool>> DeleteBookAsync(int bookId);


    }
}
