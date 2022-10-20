using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/products")]
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
        public IActionResult GetProducts()
        {
            var products = _repository.Product.GetAllProducts(trackChanges: false);
            var productDto = products.Select(c => new ProductDto 
            {
                Id = c.Id,
                ManufacturerId = c.ManufacturerId,
                NameModels = c.NameModels,
                Price = c.Price,
            }).ToList();
            return Ok(productDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _repository.Product.GetProducts(id, trackChanges: false);
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
    }
}
