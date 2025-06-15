using AutoMapper;
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
    public class BorrowedBooksController : ControllerBase
    {
        private readonly IBorrowedBookService _borrowedBookService;
    
        public BorrowedBooksController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
      
        }
        [HttpPost]
        public async Task<IActionResult> CreateBorrowing([FromQuery] int bookId, [FromQuery] int borrowerId, [FromBody] BorrowedBookDto borrowedBookModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _borrowedBookService.CreateBorrowingAsync(bookId,borrowerId,borrowedBookModel);

            return StatusCode(response.StatusCode, response.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetBorrowedBooks()
            => Ok(await _borrowedBookService.GetBorrowedBooksAsync());

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBorrowersByBookId(int bookId)
        {
            var borrowers = await _borrowedBookService.GetBorrowersByBookIdAsync(bookId);

            return borrowers.Success ? StatusCode(borrowers.StatusCode, borrowers.Data) : StatusCode(borrowers.StatusCode, borrowers.Message);
        }
        [HttpGet("/BorrowerBooks/{borrowerId}")]
        public async Task<IActionResult> GetBorrowedBookByBorrowerId(int borrowerId)
        {
            var borrowedBooks = await _borrowedBookService.GetBorrowedBookByBorrowerIdAsync(borrowerId);

            return borrowedBooks.Success ? StatusCode(borrowedBooks.StatusCode, borrowedBooks.Data) : StatusCode(borrowedBooks.StatusCode, borrowedBooks.Message);

        }
        [HttpDelete("{bookId},{borrowerId}")]
        public async Task<IActionResult> DeleteBorrowedBook(int bookId, int borrowerId)
        {
            var response = await _borrowedBookService.DeleteBorrowedBookAsync(bookId,borrowerId);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
