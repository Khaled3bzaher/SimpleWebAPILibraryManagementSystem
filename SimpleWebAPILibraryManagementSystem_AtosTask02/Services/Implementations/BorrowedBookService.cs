using AutoMapper;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;
using System.Net;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly IMapper _mapper;

        public BorrowedBookService(IUnitOfWork unitOfWork, IBorrowedBookRepository borrowedBookRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _borrowedBookRepository = borrowedBookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateBorrowingAsync(int bookId, int borrowerId, BorrowedBookDto borrowedBookModel)
        {
            if (borrowedBookModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");


            if (await _borrowedBookRepository.isBorrowed(bookId, borrowerId))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "Already Borrowed Before..!");


            var selectedbook = await _unitOfWork.Repository<Book>().GetByIdAsync(bookId);
            if (selectedbook is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Book Entered Not Found");

            var bookBorrower = await _unitOfWork.Repository<Borrower>().GetByIdAsync(borrowerId);
            if (bookBorrower is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Borrower Entered Not Found");


            if (borrowedBookModel.ReturnedDate.HasValue && (borrowedBookModel.ReturnedDate < borrowedBookModel.BorrowedDate))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "Return Date Must Be Larger Than Borrowing Date..!");


            var borrowedBookMap = _mapper.Map<BorrowedBook>(borrowedBookModel);
            borrowedBookMap.Borrower = bookBorrower;
            borrowedBookMap.BorrowerId = borrowerId;
            borrowedBookMap.Book = selectedbook;
            borrowedBookMap.BookId = bookId;

            if (!await _borrowedBookRepository.AddAsync(borrowedBookMap))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Created");
        }

        public async Task<ApiResponse<bool>> DeleteBorrowedBookAsync(int bookId, int borrowerId)
        {
            var borrowedBook = await _borrowedBookRepository.GetBorrowedBook(bookId, borrowerId);

            if (borrowedBook is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Borrowed Book Entered Not Found");

            if (!await _borrowedBookRepository.DeleteAsync(borrowedBook))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Deleted");
        }

        public async Task<ApiResponse<List<DisplayBorrowedBook>>> GetBorrowedBookByBorrowerIdAsync(int borrowerId)
        {
            var books = await _borrowedBookRepository.GetDisplayBorrowedBookByBorrowerId(borrowerId);

            if (books.Count == 0)
                return ApiResponse<List<DisplayBorrowedBook>>.FailureResponse(false, StatusCodes.Status404NotFound, "Borrower Not Found");

            return ApiResponse<List<DisplayBorrowedBook>>.SuccessResponse(true, StatusCodes.Status200OK, books);
        }

        public async Task<List<DisplayBorrowedBook>> GetBorrowedBooksAsync()
        {
            return await _borrowedBookRepository.GetDisplayBorrowedBooks();
        }

        public async Task<ApiResponse<List<DisplayBorrowedBook>>> GetBorrowersByBookIdAsync(int bookId)
        {
            var books = await _borrowedBookRepository.GetDisplayBorrowersByBookId(bookId);

            if (books.Count == 0)
                return ApiResponse<List<DisplayBorrowedBook>>.FailureResponse(false, StatusCodes.Status404NotFound, "Book Not Found");

            return ApiResponse<List<DisplayBorrowedBook>>.SuccessResponse(true, StatusCodes.Status200OK, books);
        }
    }
}
