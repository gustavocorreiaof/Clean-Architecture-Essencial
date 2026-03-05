using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productAppService,
            ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productAppService;
            _categoryService = categoryService;
            _environment = environment;


        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetByIdAsync(id.Value);

            if (productDto == null) return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetByIdAsync(id.Value);

            if (productDto == null) return NotFound();

            return View(productDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.RemoveProductAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetByIdAsync(id.Value);

            if (productDto == null) return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDto);
        }
    }
}
