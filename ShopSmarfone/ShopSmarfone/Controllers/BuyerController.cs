using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.DataShaping;
using ShopSmarfone.ActionFilters;

namespace ShopSmarfone.Controllers
{
    [Route("api/buyers")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BuyerDto> _dataShaper;

        public BuyerController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<BuyerDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;


        }
        [HttpGet]
        [HttpHead]
        public async Task <IActionResult> GetBuyer([FromQuery] BuyerParameters buyerParameters)
        {
             var buyers = await _repository.Buyer.GetAllBuyerAsync(false, buyerParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(buyers.MetaData));
            var buyersDto = _mapper.Map<IEnumerable<BuyerDto>>(buyers);
            return Ok(_dataShaper.ShapeData(buyersDto, buyerParameters.Fields));


        }
        [HttpGet("{id}", Name = "BuyerById")]
        [HttpHead("{id}")]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> CreateBuyer([FromBody] BuyerForCreationDto buyer)
        {
            var buyerEntity = _mapper.Map<Buyer>(buyer);
            _repository.Buyer.CreateBuyer(buyerEntity);
            await _repository.SaveAsync();
            var buyerToReturn = _mapper.Map<BuyerDto>(buyerEntity);
            return CreatedAtRoute("BuyerById", new { id = buyerToReturn.Id }, buyerToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateBuyerExistsAttribute))]
        public async Task <IActionResult> DeleteBuyer(Guid id)
        {
            var buyer = HttpContext.Items["buyer"] as Buyer;
            _repository.Buyer.DeleteBuyer(buyer);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateBuyerExistsAttribute))]
        public async Task <IActionResult> UpdateBuyer(Guid id, [FromBody] BuyerForUpdateDto buyer)
        {
            var buyerEntity = HttpContext.Items["buyer"] as Buyer;
            _mapper.Map(buyer, buyerEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, DELETE, PUT, PATCH");
            return Ok();
        }

    }
}
