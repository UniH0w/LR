using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopSmarfone.ActionFilters;
using System.Net.Sockets;

namespace ShopSmarfone.Controllers
{
    [Route("api/product/{ProductID}/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<StorageDto> _dataShaper;
        public StorageController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<StorageDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }
        /// <summary>
        /// Показывает список склада
        /// </summary>
        /// <param name="ProductId">Id продукта</param>
        /// <param name="storageParameters">Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [HttpHead]
        public async Task <IActionResult> GetStorageForProduct(Guid ProductId, [FromQuery] StorageParameters storageParameters)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var productFromDb = await _repository.Storage.GetAllStorageAsync(ProductId, storageParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(productFromDb.MetaData));
            var productDto = _mapper.Map<IEnumerable<StorageDto>>(productFromDb);
            return Ok(_dataShaper.ShapeData(productDto, storageParameters.Fields));
        }
        /// <summary>
        /// Показывает список склада по id
        /// </summary>
        /// <param name="ProductId">Id продукта</param>
        /// <param name="id">Id склада</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetStorageForProduct"), Authorize]
        [HttpHead("{id}")]
        public async Task <IActionResult> GetStorageForCProduct(Guid ProductId, Guid id)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageDb = await _repository.Storage.GetStorageAsync(ProductId, id,
           trackChanges:
            false);
            if (storageDb == null)
            {
                _logger.LogInfo($"Storage with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var storage = _mapper.Map<StorageDto>(storageDb);
            return Ok(storage);
        }
        /// <summary>
        /// Создаёт новые данные для склада
        /// </summary>
        /// <param name="ProductId">Id продукта</param>
        /// <param name="storage"> Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> CreateStorageForProduct(Guid ProductId, [FromBody] StorageForCreationDto storage)
        {
            var product = await _repository.Product.GetProductsAsync(ProductId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Company with id: {ProductId} doesn't exist in the database.");
                return NotFound();
            }
            var storageEntity = _mapper.Map<Storage>(storage);
            _repository.Storage.CreateStorage(ProductId, storageEntity);
            await _repository.SaveAsync();
            var storageToReturn = _mapper.Map<StorageDto>(storageEntity);
            return CreatedAtRoute("GetStorageForProduct", new
            {
                ProductId,
                id = storageToReturn.Id
            }, storageToReturn);
        }
        /// <summary>
        /// Удаляет  данные склада
        /// </summary>
        /// <param name="ProductId">Id продукта</param>
        /// <param name="id"> id склада</param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateStorageExistsAttribute))]
        public async Task <IActionResult> DeleteEmployeeForCompany(Guid ProductId, Guid id)
        {
            var storageForCompany = HttpContext.Items["storage"] as Storage;
            _repository.Storage.DeleteStorage(storageForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Обновляет  данные склада
        /// </summary>
        /// <param name="ProductId">Id продукта</param>
        /// <param name="id"> id склада</param>
        /// <param name="storage">Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateStorageExistsAttribute))]
        public async Task <IActionResult> UpdateStorageForProduct(Guid ProductId, Guid id, [FromBody] StorageForUpdateDto storage)
        { 
            var storageEntity = HttpContext.Items["storage"] as Storage;
            _mapper.Map(storage, storageEntity);
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
