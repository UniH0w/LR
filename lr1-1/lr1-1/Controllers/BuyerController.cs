using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/buyer")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BuyerController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;


        }
        [HttpGet]
        public IActionResult GetBuyer()
        {
            var buyer = _repository.Buyer.GetAllBuyer(trackChanges: false);
            var buyerDto = buyer.Select(c => new BuyerDto
            {
                Id = c.Id,
                Family = c.Family,
                Name = c.Name,
                MiddleName = c.MiddleName,
            }).ToList();
            return Ok(buyerDto);
        }

        [HttpGet("{id}", Name = "BuyerById")]
        public IActionResult GetBuyer(Guid id)
        {
            var buyer = _repository.Buyer.GetBuyer(id, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var buyerDto = _mapper.Map<BuyerDto>(buyer);
                return Ok(buyerDto);
            }
        }
        [HttpPost]
        public IActionResult CreateBuyer([FromBody] BuyerForCreationDto buyer)
        {
            if (buyer == null)
            {
                _logger.LogError("BuyerForCreationDto object sent from client is null.");
                return BadRequest("BuyerForCreationDto object is null");
            }
            var buyerEntity = _mapper.Map<Buyer>(buyer);
            _repository.Buyer.CreateBuyer(buyerEntity);
            _repository.Save();
            var buyerToReturn = _mapper.Map<BuyerDto>(buyerEntity);
            return CreatedAtRoute("BuyerById", new { id = buyerToReturn.Id }, buyerToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBuyer(Guid id)
        {
            var buyer = _repository.Buyer.GetBuyer(id, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Buyer.DeleteBuyer(buyer);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdatebBuyer(Guid id, [FromBody] BuyerForUpdateDto buyer)
        {
            if (buyer == null)
            {
                _logger.LogError("BuyerForUpdateDto object sent from client is null.");
                return BadRequest("BuyerForUpdateDto object is null");
            }
            var buyerEntity = _repository.Buyer.GetBuyer(id, trackChanges: true);
            if (buyerEntity == null)
            {
                _logger.LogInfo($"Sklad with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(buyer, buyerEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
