using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(categoryDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(categoryDTO);
                }
                catch (Exception)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoryDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet()]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var categoryDTO = await _categoryService.GetByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }

            return View(categoryDTO);
        }
    }
}
