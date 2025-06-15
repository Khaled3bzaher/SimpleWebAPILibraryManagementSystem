using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces
{
    public interface IBorrowerService
    {
        Task<List<BorrowerDto>> GetBorrowersAsync();

        Task<ApiResponse<BorrowerDto>> GetBorrowerAsync(int borrowerId);
        Task<ApiResponse<bool>> EditBorrowerAsync(int borrowerId, BorrowerDto borrowerModel);
        Task<ApiResponse<bool>> DeleteBorrowerAsync(int borrowerId);
        Task<ApiResponse<bool>> CreateBorrowerAsync(BorrowerDto borrowerModel);


    }
}
