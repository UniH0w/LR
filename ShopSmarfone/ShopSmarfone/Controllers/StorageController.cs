using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopSmarfone.Controllers
{
    [Route("api/product/{ProductID}/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public StorageController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> GetStorageForProduct(Guid ProductId)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var productFromDb = await _repository.Storage.GetAllStorageAsync(ProductId, trackChanges: false);
            var productDto = _mapper.Map<IEnumerable<StorageDto>>(productFromDb);
            return Ok(productDto);
        }
        [HttpGet("{id}", Name = "GetStorageForProduct")]
        public async Task <IActionResult> GetStorageForCProduct(Guid ProductId, Guid id)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageDb = await _repository.Storage.GetStorageAsync(ProductId, id,
           trackChanges:
            false);
            if (storageDb == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var storage = _mapper.Map<StorageDto>(storageDb);
            return Ok(storage);
        }
        [HttpPost]
        public async Task <IActionResult> CreateStorageForProduct(Guid ProductId, [FromBody] StorageForCreationDto storage)
        {
            if (storage == null)
            {
                _logger.LogError("StorageForCreationDto object sent from client is null.");
                return BadRequest("StorageForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Company with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageEntity = _mapper.Map<Storage>(storage);
            _repository.Storage.CreateStorage(ProductId, storageEntity);
            await _repository.SaveAsync();
            var storageToReturn = _mapper.Map<StorageDto>(storageEntity);
            return CreatedAtRoute("GetStorageForProduct", new
            {
                ProductId,
                id = storageToReturn.Id
            }, storageToReturn);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteEmployeeForCompany(Guid ProductId, Guid id)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageForCompany = await _repository.Storage.GetStorageAsync(ProductId, id, false);
            if (storageForCompany == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Storage.DeleteStorage(storageForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateStorageForProduct(Guid ProductId, Guid id, [FromBody] StorageForUpdateDto storage)
        {
            if (storage == null)
            {
                _logger.LogError("StorageForUpdateDto object sent from client is null.");
                return BadRequest("StorageForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($" Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageEntity = await _repository.Storage.GetStorageAsync(ProductId, id, trackChanges: true);
            if (storageEntity == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(storage, storageEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
