using AutoMapper;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateAuthorAsync(AuthorDto authorModel)
        {
            if (authorModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");


            if (authorModel.Id != 0)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "DON'T ENTER ID IN CREATING..!");



            var authorMap = _mapper.Map<Author>(authorModel);

            if (!await _unitOfWork.Repository<Author>().AddAsync(authorMap))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Created");
        }

        public async Task<ApiResponse<bool>> DeleteAuthorAsync(int authorId)
        {
            var author = await _unitOfWork.Repository<Author>().GetByIdAsync(authorId);

            if (author is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Not Found");

            if (!await _unitOfWork.Repository<Author>().DeleteAsync(author))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Deleted");
        }

        public async Task<ApiResponse<bool>> EditAuthorAsync(int authorId, AuthorDto authorModel)
        {
            if (authorModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");


            if (authorModel.Id != authorId)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Id doesn't Match Model Id");

            if (!await _unitOfWork.Repository<Author>().isExistById(authorId))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Not Found");

            var author = _mapper.Map<Author>(authorModel);

            if (!await _unitOfWork.Repository<Author>().UpdateAsync(author))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Edited");
        }

        public async Task<ApiResponse<AuthorDto>> GetAuthorAsync(int authorId)
        {

            var author = await _unitOfWork.Repository<Author>().GetByIdAsync(authorId);

            if (author is null)
                return ApiResponse<AuthorDto>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Not Found ..!");

            var authorMap = _mapper.Map<AuthorDto>(author);

            return ApiResponse<AuthorDto>.SuccessResponse(true, StatusCodes.Status200OK, authorMap);

        }

        public async Task<List<AuthorDto>> GetAuthorsAsync()
        {
            return _mapper.Map<List<AuthorDto>>(await _unitOfWork.Repository<Author>().GetAllAsync());
        }


    }
}
