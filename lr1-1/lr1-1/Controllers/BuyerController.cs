using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
            var buyerDto = _mapper.Map<IEnumerable<BuyerDto>>(buyer);
            return Ok(buyerDto);

        }
    }
}
