using AutoMapper;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateBookAsync(int autherId, int categoryId, BookDto bookModel)
        {
            if (bookModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");

            if (bookModel.Id != 0)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status422UnprocessableEntity, "DON'T ENTER ID IN CREATING..!");

            var selectedCategory = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);

            if (selectedCategory is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Entered Not Found");

            var bookAuthor = await _unitOfWork.Repository<Author>().GetByIdAsync(autherId);
            if (bookAuthor is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Entered Not Found");


            var bookMap = _mapper.Map<Book>(bookModel);
            bookMap.Author = bookAuthor;
            bookMap.Category = selectedCategory;

            if (!await _bookRepository.AddAsync(bookMap))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Created");
        }

        public async Task<ApiResponse<bool>> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Book Not Found");

            if (!await _bookRepository.DeleteAsync(book))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Deleted");
        }

        public async Task<ApiResponse<bool>> EditBookAsync(int bookId, int autherId, int categoryId, BookDto bookModel)
        {

            if (bookModel is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Body is Empty");


            if (bookModel.Id != bookId)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status400BadRequest, "Id doesn't Match Model Id");

            if (!await _bookRepository.isExistById(bookId))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Book Not Found");

            var selectedCategory = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);
            if (selectedCategory is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Not Found");

            var bookAuthor = await _unitOfWork.Repository<Author>().GetByIdAsync(autherId);
            if (bookAuthor is null)
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Entered Not Found");

            var bookMap = _mapper.Map<Book>(bookModel);
            bookMap.Author = bookAuthor;
            bookMap.Category = selectedCategory;

            if (!await _bookRepository.UpdateAsync(bookMap))
                return ApiResponse<bool>.FailureResponse(false, StatusCodes.Status500InternalServerError, "Something went wrong While Saving..!");

            return ApiResponse<bool>.SuccessResponse(true, StatusCodes.Status200OK, true, "Successfully Edited");

        }

        public async Task<ApiResponse<List<DisplayBookDto>>> GetAuthorBooksAsync(int authorId)
        {
            var books = await _bookRepository.GetAllAuthorBooks(authorId);

            if (books.Count == 0)
                return ApiResponse<List<DisplayBookDto>>.FailureResponse(false, StatusCodes.Status404NotFound, "Author Not Found ..!");

            return ApiResponse<List<DisplayBookDto>>.SuccessResponse(true, StatusCodes.Status200OK, books);
        }

        public async Task<ApiResponse<DisplayBookDto>> GetBookAsync(int bookId)
        {
            var book = await _bookRepository.GetDisplayBookById(bookId);

            if (book is null)
                return ApiResponse<DisplayBookDto>.FailureResponse(false, StatusCodes.Status404NotFound, "Book Not Found ..!");

            return ApiResponse<DisplayBookDto>.SuccessResponse(true, StatusCodes.Status200OK, book);
        }

        public async Task<List<DisplayBookDto>> GetBooksAsync()
        {
            return await _bookRepository.GetDisplayBooks();
        }

        public async Task<ApiResponse<List<DisplayBookDto>>> GetCategoryBooksAsync(int categoryId)
        {
            var books = await _bookRepository.GetAllCategoryBooks(categoryId);

            if (books.Count == 0)
                return ApiResponse<List<DisplayBookDto>>.FailureResponse(false, StatusCodes.Status404NotFound, "Category Not Found ..!");

            return ApiResponse<List<DisplayBookDto>>.SuccessResponse(true, StatusCodes.Status200OK, books);

        }
    }
}
