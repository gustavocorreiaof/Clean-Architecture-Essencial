using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
                return NotFound("No categories found");

            return Ok(categories);
        }

        [HttpGet("{id:guid}", Name = "GetById")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return BadRequest("There is no category with given Id.");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Category data is null.");

            await _categoryService.CreateCategoryAsync(categoryDTO);

            return new CreatedAtRouteResult("GetById", new { Id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest("Id in the URL does not match Id in the body.");

            if (categoryDTO == null)
                return BadRequest();

            await _categoryService.UpdateCategoryAsync(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<CategoryDTO>> Delete(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            
            if (category == null)
                return BadRequest("There is no category with given Id.");
            
            await _categoryService.RemoveCategoryAsync(id);
            
            return Ok(category);
        }
    }
}
