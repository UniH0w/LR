using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace lr1_1.Controllers
{
    [Route("api/buyer/{BuyerId}/product/{ProductId}/order")]
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
            public IActionResult OrderForProduct(Guid BuyerId, Guid ProductId)
            {
                var buyer = _repository.Buyer.GetBuyer(BuyerId, trackChanges: false);
                if (buyer == null)
                {
                    _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                    return NotFound();
                }
                var order = _repository.Product.GetAllProducts(ProductId, trackChanges: false);
                if (order == null)
                {
                    _logger.LogInfo($"Buyery with id: {BuyerId} doesn't exist in the database.");
                    return NotFound();
                }

                var orderFromDb = _repository.Order.GetAllOrder(BuyerId, false);
                var orderDto = _mapper.Map<IEnumerable<BuyerDto>>(orderFromDb);
                return Ok(orderFromDb);
            }

        [HttpGet("{id}", Name = "GetOder")]
        public IActionResult GetOrder(Guid ProductId, Guid BuyerId, Guid id)
        {
            var buyer = _repository.Buyer.GetBuyer(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var product = _repository.Product.GetAllProducts(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var orderDb = _repository.Order.GetOrder(BuyerId, id, false);
            if (orderDb == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var order = _mapper.Map<OrderDto>(orderDb);
            return Ok(order);
            }

            [HttpPost]
            public IActionResult CreatePlan(Guid BuyerId, Guid ProductId, [FromBody] OrderForCreationDto order)
            {
                if (order == null)
                {
                    _logger.LogError("OrderCreationDto object sent from client is null.");
                    return BadRequest("OrderCreationDto object is null");
                }
                var buyer = _repository.Buyer.GetBuyer(BuyerId, trackChanges: false);
                if (buyer == null)
                {
                    _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                    return NotFound();
                }
                var product = _repository.Product.GetAllProducts(ProductId, trackChanges: false);
                if (product == null)
                {
                    _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                    return NotFound();
                }
                var orderEntity = _mapper.Map<Order>(order);
                _repository.Order.CreateOrder(ProductId, BuyerId, orderEntity);
                _repository.Save();
                var orderReturn = _mapper.Map<OrderDto>(orderEntity);
                return CreatedAtRoute("GetPlan", new
                {
                    ProductId,
                    BuyerId,
                    orderReturn.Id
                }, orderReturn);
            }
        }
    }
 

