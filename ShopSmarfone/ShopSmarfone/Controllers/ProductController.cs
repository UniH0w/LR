using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopSmarfone.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetProduct()
        {
            try
            {
                var products = await _repository.Product.GetAllProductsAsync(trackChanges: false);
                var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetProducts)} action {ex}  ");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "ProductById")]
        public async Task <IActionResult> GetProducts(Guid id)
        {
            var product = await _repository.Product.GetProductsAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
        }
        [HttpPost]
        public async Task <IActionResult> CreateBuyer([FromBody] ProductForCreationDto product)
        {
            if (product == null)
            {
                _logger.LogError("ProductForCreationDto object sent from client is null.");
                return BadRequest("ProductForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var productEntity = _mapper.Map<Product>(product);
            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("ProductById", new { id = productToReturn.Id }, productToReturn);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repository.Product.GetProductsAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateProduct(Guid id, [FromBody] ProdutctForUpdateDto product)
        {
            if (product == null)
            {
                _logger.LogError("ProductForUpdateDto object sent from client is null.");
                return BadRequest("ProductForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var productEntity = await _repository.Product.GetProductsAsync(id, trackChanges: true);
            if (productEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }

}
