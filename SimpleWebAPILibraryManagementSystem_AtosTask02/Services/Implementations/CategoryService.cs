using AutoMapper;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateCategoryAsync(CategoryDto categoryModel)
        {

            if (categoryModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");

            if (categoryModel.Id != 0)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "DON'T ENTER ID IN CREATING..!");

            if (await _unitOfWork.Repository<Category>()
                .isExistByName(categoryModel.Name))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "Category Exists Before..!");
            

            var categoryMap = _mapper.Map<Category>(categoryModel);

            if (!await _unitOfWork.Repository<Category>().AddAsync(categoryMap))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Created");
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);

            if (category is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Not Found");


            if (!await _unitOfWork.Repository<Category>().DeleteAsync(category))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Deleting..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Deleted");
        }

        public async Task<ApiResponse<bool>> EditCategoryAsync(int categoryId, CategoryDto categoryModel)
        {
            if (categoryModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");

            if (categoryModel.Id != categoryId)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Id doesn't Match Model Id");

            if (!await _unitOfWork.Repository<Category>().isExistById(categoryId))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Not Found");

            if (await _unitOfWork.Repository<Category>().isExistByName(categoryModel.Name))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "Category Exists Before..!");

            var category = _mapper.Map<Category>(categoryModel);

            if (!await _unitOfWork.Repository<Category>().UpdateAsync(category))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Edited");
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return _mapper.Map<List<CategoryDto>>(await _unitOfWork.Repository<Category>().GetAllAsync());
        }

        public async Task<ApiResponse<CategoryDto>> GetCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);

            if (category is null)
                return ApiResponse<CategoryDto>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Not Found ..!");

            var categoryMap = _mapper.Map<CategoryDto>(category);

            return ApiResponse<CategoryDto>.SuccessResponse(true, StatusCodes.Status200OK, categoryMap);

        }
    }
}
