using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ShopSmarfone.Controllers
{
    [Route("api/buyers/{BuyerId}/order")]
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
        public async Task <IActionResult> OrderForProduct(Guid BuyerId)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var OrderFromDb = await _repository.Order.GetAllOrderAsync(BuyerId, false);
            var OrderDto = _mapper.Map<IEnumerable<OrderDto>>(OrderFromDb);
            return Ok(OrderDto);
        }

        [HttpGet("{id}", Name = "GetOderForBuyer")]
        public async Task <IActionResult> GetOrderForBuyer(Guid BuyerId, Guid id)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var orderDb = await _repository.Order.GetOrderAsync(BuyerId, id, false);
            if (orderDb == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var order = _mapper.Map<OrderDto>(orderDb);
            return Ok(order);
        }

        [HttpPost]
        public async Task <IActionResult> CreateOrder(Guid BuyerId, [FromBody] OrderForCreationDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderCreationDto object sent from client is null.");
                return BadRequest("OrderCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            _repository.Order.CreateOrder(BuyerId, orderEntity);
             await _repository.SaveAsync();
            var orderReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtAction("CreateOrder", new
            {
                BuyerId,
                orderReturn.Id
            }, orderReturn);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteOrderForBuyer(Guid BuyerId, Guid id)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Sklad with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }

            var orderForbuyer = await _repository.Order.GetOrderAsync(BuyerId, id, trackChanges: false);
            if (orderForbuyer == null)
            {
                _logger.LogInfo($"Plan with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Order.DeleteOrder(orderForbuyer);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateOrderForBuyer(Guid BuyerId, Guid id, [FromBody] OrderForUpdateDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForUpdateDto object sent from client is null.");
                return BadRequest("OrderForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = await _repository.Order.GetOrderAsync(BuyerId, id, trackChanges: true);
            if (orderEntity == null)
            {
                _logger.LogInfo($" Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(order, orderEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task <IActionResult> PatchUpdateOrder(Guid BuyerId, Guid id, [FromBody] JsonPatchDocument<OrderForUpdateDto> order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForUpdateDto object sent from client is null.");
                return BadRequest("OrderForUpdateDto object is null");
            }
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }

            var orderEntity = await _repository.Order.GetOrderAsync(BuyerId, id, true);
            if (orderEntity == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderEntity);
            order.ApplyTo(orderToPatch, ModelState);
            TryValidateModel(orderToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(orderToPatch, orderEntity);
            await _repository.SaveAsync();
            return NoContent();
        }



    }
}
