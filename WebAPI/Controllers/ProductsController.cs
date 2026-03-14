using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtos = await _productService.GetAllProductsAsync();
            if (produtos == null)
            {
                return NotFound("Products not found");
            }
            return Ok(produtos);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(Guid id)
        {
            var produto = await _productService.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound("Product not found");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Data Invalid");

            await _productService.CreateProductAsync(produtoDto);

            return new CreatedAtRouteResult("GetProduct",
                new { id = produtoDto.Id }, produtoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProductDTO produtoDto)
        {
            if (id != produtoDto.Id)
            {
                return BadRequest("Data invalid");
            }

            if (produtoDto == null)
                return BadRequest("Data invalid");

            await _productService.UpdateProductAsync(produtoDto);

            return Ok(produtoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(Guid id)
        {
            var produtoDto = await _productService.GetByIdAsync(id);

            if (produtoDto == null)
            {
                return NotFound("Product not found");
            }

            await _productService.RemoveProductAsync(id);

            return Ok(produtoDto);
        }
    }
}
