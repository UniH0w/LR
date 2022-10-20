using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OrderController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;


        }
        [HttpGet]
        public IActionResult GetOrder()
        {

            var order = _repository.Order.GetAllOrder(trackChanges: false);
            var orderDto = order.Select(c => new OrderDto 
            { 
              Id = c.Id,
              IdProduct1 = c.IdProduct1,
              IdBuyer1 = c.IdBuyer1,
            }).ToList();
            return Ok(orderDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(Guid id)
        {
            var order = _repository.Order.GetOrder(id, trackChanges: false);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                return Ok(orderDto);
            }
        }
    }
}
