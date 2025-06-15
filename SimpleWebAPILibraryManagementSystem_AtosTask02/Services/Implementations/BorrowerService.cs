using AutoMapper;
using Azure;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateBorrowerAsync(BorrowerDto borrowerModel)
        {
            if (borrowerModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");

            if (borrowerModel.Id != 0)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "DON'T ENTER ID IN CREATING..!");

            var borrowerMap = _mapper.Map<Borrower>(borrowerModel);

            if (!await _unitOfWork.Repository<Borrower>().AddAsync(borrowerMap))
            {
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");
            }
            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Created");
        }

        public async Task<ApiResponse<bool>> DeleteBorrowerAsync(int borrowerId)
        {
            var borrower = await _unitOfWork.Repository<Borrower>().GetByIdAsync(borrowerId);

            if (borrower is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Borrower Not Found");

            if (!await _unitOfWork.Repository<Borrower>().DeleteAsync(borrower))
            {
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Deleting..!");
            }
            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Deleted");
        }

        public async Task<ApiResponse<bool>> EditBorrowerAsync(int borrowerId, BorrowerDto borrowerModel)
        {
            if (borrowerModel is null)
                return ApiResponse<bool>.FailureResponse(false,StatusCodes.Status400BadRequest, "Body is Empty");

            if (borrowerModel.Id != borrowerId)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Id doesn't Match Model Id");

            if (!await _unitOfWork.Repository<Borrower>().isExistById(borrowerId))
                return ApiResponse<bool>.FailureResponse(false,StatusCodes.Status404NotFound ,"Borrower Not Found");

            var borrower = _mapper.Map<Borrower>(borrowerModel);

            if (!await _unitOfWork.Repository<Borrower>().UpdateAsync(borrower))
            {
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");
            }
            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK,true,"Successfully Edited");
        }

        public async Task<ApiResponse<BorrowerDto>> GetBorrowerAsync(int borrowerId)
        {
            var borrower = await _unitOfWork.Repository<Borrower>().GetByIdAsync(borrowerId);

            if (borrower is null)
                return ApiResponse<BorrowerDto>.FailureResponse(false, StatusCodes.Status404NotFound, "Borrower Not Found ..!");

            var borrowerMap = _mapper.Map<BorrowerDto>(borrower);

            return ApiResponse<BorrowerDto>.SuccessResponse(true, StatusCodes.Status200OK,borrowerMap);

        }

        public async Task<List<BorrowerDto>> GetBorrowersAsync()
        {
            return _mapper.Map<List<BorrowerDto>>(await _unitOfWork.Repository<Borrower>().GetAllAsync());
        }


    }
}
