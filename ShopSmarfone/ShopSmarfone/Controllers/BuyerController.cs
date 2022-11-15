using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopSmarfone.Controllers
{
    [Route("api/buyers")]
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
        public async Task <IActionResult> GetBuyer()
        {
            try
            {
                var buyers = await _repository.Buyer.GetAllBuyerAsync(trackChanges: false);
                var buyersDto = _mapper.Map<IEnumerable<BuyerDto>>(buyers);
                return Ok(buyersDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetBuyers)} action {ex}  ");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "BuyerById")]
        public async Task <IActionResult> GetBuyers(Guid id)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(id, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var buyerDto = _mapper.Map<BuyerDto>(buyer);
                return Ok(buyerDto);
            }
        }
        [HttpPost]
        public async Task <IActionResult> CreateBuyer([FromBody] BuyerForCreationDto buyer)
        {
            if (buyer == null)
            {
                _logger.LogError("BuyerForCreationDto object sent from client is null.");
                return BadRequest("BuyerForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var buyerEntity = _mapper.Map<Buyer>(buyer);
            _repository.Buyer.CreateBuyer(buyerEntity);
            await _repository.SaveAsync();
            var buyerToReturn = _mapper.Map<BuyerDto>(buyerEntity);
            return CreatedAtRoute("BuyerById", new { id = buyerToReturn.Id }, buyerToReturn);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteBuyer(Guid id)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(id, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Buyer.DeleteBuyer(buyer);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateBuyer(Guid id, [FromBody] BuyerForUpdateDto buyer)
        {
            if (buyer == null)
            {
                _logger.LogError("buyerForUpdateDto object sent from client is null.");
                return BadRequest("buyerForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var buyerEntity =  await _repository.Buyer.GetBuyerAsync(id, trackChanges: true);
            if (buyerEntity == null)
            {
                _logger.LogInfo($"buyer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(buyer, buyerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

    }
}
