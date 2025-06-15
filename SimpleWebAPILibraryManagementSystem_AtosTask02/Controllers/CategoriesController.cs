using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IUnitOfWork unitOfWork,IMapper mapper,ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _categoryService.CreateCategoryAsync(categoryModel);

            return StatusCode(response.StatusCode, response.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
            => Ok(await _categoryService.GetCategoriesAsync());

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);

            return category.Success ? StatusCode(category.StatusCode, category.Data) : StatusCode(category.StatusCode, category.Message);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] CategoryDto categoryModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Model is Not Valid");

            var response = await _categoryService.EditCategoryAsync(categoryId, categoryModel);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
