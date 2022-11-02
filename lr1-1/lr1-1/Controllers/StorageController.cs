using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/buyer/{BuyerId}/storage")]
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
        public IActionResult GetStorage(Guid BuyerId)
        {
            var storage = _repository.Storage.GetAllStorage(BuyerId, trackChanges: false);
            if (storage == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }

            var storageFromDb = _repository.Storage.GetAllStorage(BuyerId, trackChanges: false);
            var storageDto = _mapper.Map<IEnumerable<StorageDto>>(storageFromDb);
            return Ok(storageFromDb);
        }
        [HttpGet("{id}", Name = "GetStorageForBuyer")]
        public IActionResult GetStorage(Guid BuyerId, Guid Id)
        {
            var buyer = _repository.Storage.GetAllStorage(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Storage with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var storageDb = _repository.Storage.GetStorage(BuyerId, Id, trackChanges: false);
            if (storageDb == null)
            {
                _logger.LogInfo($"Storage with id: {Id} doesn't exist in the database.");
                return NotFound();
            }
            var storage = _mapper.Map<StorageDto>(storageDb);
            return Ok(storage);
        }
        [HttpPost]
        public IActionResult CreateProductForBuyer(Guid BuyerId, [FromBody] StorageForCreationDto storage)
        {
            if (storage == null)
            {
                _logger.LogError("StorageForCreationDto object sent from client is null.");
                return BadRequest("StorageForCreationDto object is null");
            }
            var Buyer = _repository.Storage.GetAllStorage(BuyerId, trackChanges: false);
            if (Buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var storageEntity = _mapper.Map<Storage>(storage);
            _repository.Storage.CreateStorage(BuyerId, storageEntity);
            _repository.Save();
            var storageToReturn = _mapper.Map<StorageDto>(storageEntity);
            return CreatedAtRoute("GetStorageForBuyer", new
            {
                BuyerId,
                id = storageToReturn.Id
            }, storageToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBuyerForStorage(Guid BuyerId, Guid id)
        {
            var buyer = _repository.Buyer.GetBuyer(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var buyerForStorage = _repository.Storage.GetStorage(BuyerId, id, trackChanges: false);
            if (buyerForStorage == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Storage.DeleteStorage(buyerForStorage);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStorageForBuyer(Guid BuyerId, Guid id, [FromBody] BuyerForUpdateDto buyer)
        {
            if (buyer == null)
            {
                _logger.LogError("BuyerForUpdateDto object sent from client is null.");
                return BadRequest("BuyerForUpdateDto object is null");
            }
            var storage = _repository.Storage.GetAllStorage(BuyerId, trackChanges: false);
            if (storage == null)
            {
                _logger.LogInfo($"Sklad with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var storageEntity = _repository.Storage.GetStorage(BuyerId, id, trackChanges:true);
            if (storageEntity == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(storage, storageEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
