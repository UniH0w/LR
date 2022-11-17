using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopSmarfone.ActionFilters;

namespace ShopSmarfone.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ProductDto> _dataShaper;
        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<ProductDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        [HttpGet]
        public async Task <IActionResult> GetProduct([FromQuery] ProductParameters productParameters)
        {
    
            var products = await _repository.Product.GetAllProductAsync(false, productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(_dataShaper.ShapeData(productDto, productParameters.Fields));

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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> CreateBuyer([FromBody] ProductForCreationDto product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("ProductById", new { id = productToReturn.Id }, productToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task <IActionResult> DeleteProduct(Guid id)
        {
            var product = HttpContext.Items["product"] as Product;
            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task <IActionResult> UpdateProduct(Guid id, [FromBody] ProdutctForUpdateDto product)
        {
            var productEntity = HttpContext.Items["product"] as Product;
            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }

}
