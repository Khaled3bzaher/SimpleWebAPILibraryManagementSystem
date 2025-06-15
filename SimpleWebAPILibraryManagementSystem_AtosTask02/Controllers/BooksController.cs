using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;
using System.Net;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromQuery]int autherId, [FromQuery] int categoryId, [FromBody] BookDto bookModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _bookService.CreateBookAsync(autherId,categoryId,bookModel);

            return StatusCode(response.StatusCode, response.Message);

        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> EditBook(int bookId,[FromQuery] int autherId, [FromQuery] int categoryId, [FromBody] BookDto bookModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _bookService.EditBookAsync(bookId,autherId,categoryId, bookModel);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
            => Ok(await _bookService.GetBooksAsync());

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var book = await _bookService.GetBookAsync(bookId);

            return book.Success ? StatusCode(book.StatusCode, book.Data) : StatusCode(book.StatusCode, book.Message);
        }

        [HttpGet("Authors/{authorId}")]
        public async Task<IActionResult> GetAuthorBooks(int authorId)
        {
            var books = await _bookService.GetAuthorBooksAsync(authorId);

            return books.Success ? StatusCode(books.StatusCode, books.Data) : StatusCode(books.StatusCode, books.Message);
        }
        [HttpGet("Categories/{categoryId}")]
        public async Task<IActionResult> GetCategoryBooks(int categoryId)
        {
            var books = await _bookService.GetCategoryBooksAsync(categoryId);

            return books.Success ? StatusCode(books.StatusCode, books.Data) : StatusCode(books.StatusCode, books.Message);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var response = await _bookService.DeleteBookAsync(bookId);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
