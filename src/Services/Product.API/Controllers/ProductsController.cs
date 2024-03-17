using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProductsAsync();
            var result = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(products);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            var productEntity = await _repository.GetProductByNo(productDTO.No);
            if (productEntity != null)
            {
                return BadRequest($"Product No: {productDTO.No} is existed.");
            }
            var product = _mapper.Map<CatelogProduct>(productDTO);
            await _repository.CreateProduct(product);
            await _repository.SaveChangeAsync();
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductDTO productDTO)
        {
            var product = await _repository.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var updateProduct = _mapper.Map(productDTO, product);
            await _repository.UpdateProduct(product);
            await _repository.SaveChangeAsync();
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct([Required] long id)
        {
            var product = await _repository.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _repository.DeleteProduct(id);
            await _repository.SaveChangeAsync();
            return NoContent();
        }
        #endregion CRUD

        #region Additional Actions
        [HttpGet("get-product-by-no/{productNo}")]
        public async Task<IActionResult> GetProductByNo(string productNo)
        {
            var product = await _repository.GetProductByNo(productNo);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ProductDTO>(product);
            return Ok(result);
        }
        #endregion
    }
}