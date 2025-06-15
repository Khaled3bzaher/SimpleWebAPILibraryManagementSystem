using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        
        private readonly IBorrowerService _borrowerService;

        public BorrowersController( IBorrowerService borrowerService)
        {
            
            _borrowerService = borrowerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBorrower([FromBody] BorrowerDto borrowerModel)
        {

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _borrowerService.CreateBorrowerAsync( borrowerModel);


            return StatusCode(response.StatusCode, response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GetBorrowers()
        {
            return Ok(await _borrowerService.GetBorrowersAsync());
        }

        [HttpGet("{borrowerId}")]
        public async Task<IActionResult> GetBorrower(int borrowerId)
        {
            var borrower = await _borrowerService.GetBorrowerAsync(borrowerId);
            return borrower.Success ? StatusCode(borrower.StatusCode, borrower.Data) : StatusCode(borrower.StatusCode, borrower.Message);
        }

        [HttpPut("{borrowerId}")]
        public async Task<IActionResult> EditBorrower(int borrowerId, [FromBody] BorrowerDto borrowerModel)
        {

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response= await _borrowerService.EditBorrowerAsync(borrowerId, borrowerModel);
            
            return StatusCode(response.StatusCode,response.Message);
        }

        [HttpDelete("{borrowerId}")]
        public async Task<IActionResult> DeleteBorrower(int borrowerId)
        {

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");


            var response= await _borrowerService.DeleteBorrowerAsync(borrowerId);

            return StatusCode(response.StatusCode, response.Message);

        }
    }
}
