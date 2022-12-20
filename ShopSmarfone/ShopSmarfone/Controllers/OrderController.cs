using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopSmarfone.ActionFilters;
using System.Net.Sockets;

namespace ShopSmarfone.Controllers
{
    [Route("api/buyers/{BuyerId}/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<OrderDto> _dataShaper;

        public OrderController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<OrderDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        /// <summary>
        /// Возвращает список заказа
        /// </summary>
        /// <param name="orderParameters">Параметра возвращения массива данных</param>
        /// <param name="BuyerId">Id покупателя</param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [HttpHead]
        public async Task <IActionResult> OrderForProduct(Guid BuyerId, [FromQuery] OrderParameters orderParameters)
        {
            var buyer = await _repository.Buyer.GetBuyerAsync(BuyerId, trackChanges: false);
            if (buyer == null)
            {
                _logger.LogInfo($"Buyer with id: {BuyerId} doesn't exist in the database.");
                return NotFound();
            }
            var OrderFromDb = await _repository.Order.GetAllOrderAsync(BuyerId,orderParameters, false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(OrderFromDb.MetaData));
            var OrderDto = _mapper.Map<IEnumerable<OrderDto>>(OrderFromDb);
            return Ok(_dataShaper.ShapeData(OrderDto, orderParameters.Fields));
        }
        /// <summary>
        /// Возвращает заказ по id
        /// </summary>
        /// <param name="id">Id заказа</param>
        /// <param name="BuyerId">Id покупателя</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetOderForBuyer"), Authorize]
        [HttpHead("{id}")]
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
        /// <summary>
        /// Создает заказ
        /// </summary>
        /// <param name="BuyerId">Id покупателя</param>
        /// <param name="order">Название покупки</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> CreateOrder(Guid BuyerId, [FromBody] OrderForCreationDto order)
        {
           
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
        /// <summary>
        /// Удаляет заказ
        /// </summary>
        /// <param name="BuyerId">Id покупателя</param>
        /// <param name="id">Id заказа</param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateOrderExistsAttribute))]
        public async Task <IActionResult> DeleteOrderForBuyer(Guid BuyerId, Guid id)
        {

            var orderForbuyer = HttpContext.Items["order"] as Order;

            _repository.Order.DeleteOrder(orderForbuyer);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Обновляет заказ
        /// </summary>
        /// <param name="order">Параметра возвращения массива данных</param>
        /// <param name="id">Id заказа</param>
        /// <param name="BuyerId">Id покупателя</param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrderExistsAttribute))]
        public async Task <IActionResult> UpdateOrderForBuyer(Guid BuyerId, Guid id, [FromBody] OrderForUpdateDto order)
        {
            var orderEntity = HttpContext.Items["order"] as Order;
            _mapper.Map(order, orderEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Обновляет сотрудника компании
        /// </summary>
        /// <param name="BuyerId">Id покупателя</param>
        /// <param name="id">Id заказа</param>
        /// <param name="order">Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpPatch("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateOrderExistsAttribute))]
        public async Task <IActionResult> PatchUpdateOrder(Guid BuyerId, Guid id, [FromBody] JsonPatchDocument<OrderForUpdateDto> order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForUpdateDto object sent from client is null.");
                return BadRequest("OrderForUpdateDto object is null");
            }
            var orderEntity = HttpContext.Items["order"] as Order;
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
        /// <summary>
        /// Возвращает заголовки запросов
        /// </summary>
        /// <returns></returns>
        [HttpOptions, Authorize]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, DELETE, PUT, PATCH");
            return Ok();
        }



    }
}
