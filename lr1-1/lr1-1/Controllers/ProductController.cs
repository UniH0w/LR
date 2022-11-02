using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/manufacturer/{ManufacturerId}/products")]
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
        public IActionResult GetProductsForManufacturer(Guid ManufacturerID)
        {
            var manufacturer = _repository.Manufacturer.GetManufacturer(ManufacturerID, trackChanges: false);
            if (manufacturer == null)
            {
                _logger.LogInfo($"Manufacturer with id: {ManufacturerID} doesn't exist in the database.");
                return NotFound();
            }

            var productsFromDb = _repository.Product.GetAllProducts(ManufacturerID, trackChanges: false);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsFromDb);
           return Ok(productsFromDb);
        }
        [HttpGet("{id}", Name = "GetProductForManufacturer")]
        public IActionResult GetProductForManufacturer(Guid ManufacturerId, Guid id)
        {
            var manufacturer = _repository.Manufacturer.GetManufacturer(ManufacturerId, trackChanges: false);
            if (manufacturer == null)
            {
                _logger.LogInfo($"Manufacturer with id: {ManufacturerId} doesn't exist in the database.");
                return NotFound();
            }
            var productDb = _repository.Product.GetProducts(ManufacturerId, id,
           trackChanges:
            false);
            if (productDb == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var product = _mapper.Map<ProductDto>(productDb);
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProductForManufacturer(Guid ManufacturerId, [FromBody] ProductForCreationDto product)
        {
            if (product == null)
            {
                _logger.LogError("ProductForCreationDto object sent from client is null.");
                return BadRequest("ProductForCreationDto object is null");
            }
            var manufacturer = _repository.Manufacturer.GetManufacturer(ManufacturerId, trackChanges: false);
            if (manufacturer == null)
            {
                _logger.LogInfo($"Manufacturer with id: {ManufacturerId} doesn't exist in the database.");
                return NotFound();
            }
            var productEntity = _mapper.Map<Product>(product);
            _repository.Product.CreateProduct(ManufacturerId, productEntity);
            _repository.Save();
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("GetProductForManufacturer", new
            {
                ManufacturerId,
                id = productToReturn.Id
            }, productToReturn);
        }

    }
}
    


