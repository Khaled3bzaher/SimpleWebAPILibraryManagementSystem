using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<bool>> CreateCategoryAsync(CategoryDto categoryModel);

        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<ApiResponse<CategoryDto>> GetCategoryAsync(int categoryId);
        Task<ApiResponse<bool>> EditCategoryAsync(int categoryId, CategoryDto categoryModel);
        Task<ApiResponse<bool>> DeleteCategoryAsync(int categoryId);

    }
}
