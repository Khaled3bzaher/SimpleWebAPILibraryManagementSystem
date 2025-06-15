using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ApiResponse<bool>> CreateAuthorAsync(AuthorDto authorModel);
        Task<List<AuthorDto>> GetAuthorsAsync();
        Task<ApiResponse<AuthorDto>> GetAuthorAsync(int authorId);
        Task<ApiResponse<bool>> EditAuthorAsync(int authorId, AuthorDto authorModel);
        Task<ApiResponse<bool>> DeleteAuthorAsync(int authorId);

    }
}
