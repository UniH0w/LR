using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/storage")]
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
        public IActionResult GetStorage()
        {
            var storage = _repository.Storage.GetAllStorage(trackChanges: false);
            var storageDto = storage.Select(c => new StorageDto 
            {
                Id = c.Id,
                BuyerId = c.BuyerID,
                Quantity = c.Quantity,
            }).ToList();
            return Ok(storageDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetStorage(Guid id)
        {
            var storage = _repository.Storage.GetStorage(id, trackChanges: false);
            if (storage == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var storageDto = _mapper.Map<StorageDto>(storage);
                return Ok(storageDto);
            }
        }
    }
}
