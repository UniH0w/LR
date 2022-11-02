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
            return CreatedAtRoute("BuyerById", new { id = buyerToReturn.Id },buyerToReturn);
        }
    }
}
