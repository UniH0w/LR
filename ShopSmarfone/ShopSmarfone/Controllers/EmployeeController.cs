using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using ShopSmarfone.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ShopSmarfone.Controllers
{
    [Route("api/companies/{companyId}/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;


        public EmployeeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;

        }
        /// <summary>
        /// Возвращает работников определенной компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="employeeParameters">Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpGet, Authorize]
        [HttpHead]
        public async Task <IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age.");
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeesFromDb = await _repository.Employee.GetEmployeeAsync(companyId, employeeParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(employeesFromDb.MetaData));
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(_dataShaper.ShapeData(employeeDto, employeeParameters.Fields));

        }
        /// <summary>
        /// Возвращает работника определенной компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="id">Id работника</param>
        /// <param name="employeeParameters">Параметра возвращения массива данных</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetEmployeeForCompany"), Authorize]
        [HttpHead("{id}")]
        public async Task <IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id,
           trackChanges:
            false);
            if (employeeDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return Ok(employee);
        }
        /// <summary>
        /// Создает нового сотрудника компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="employee">Данные работника</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            
            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                id = employeeToReturn.Id
            }, employeeToReturn);
        }
        /// <summary>
        /// Удаляет сотрудника компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="id">Id работника</param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize] 
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public async Task <IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            var employeeForCompany = await _repository.Employee.GetEmployeeAsync(companyId, id, false);
            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Обновляет сотрудника компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="id">Id работника</param>
        /// <param name="employee">Новые данные работника</param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task <IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            
            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges: true);
            _mapper.Map(employee, employeeEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Обновляет сотрудника компании
        /// </summary>
        /// <param name="companyId">Id компании</param>
        /// <param name="id">Id работника</param>
        /// <param name="patchDoc">Новые данные работника</param>
        /// <returns></returns>
        [HttpPatch("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public async Task <IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var employeeEntity = HttpContext.Items["employee"] as Employee;
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            patchDoc.ApplyTo(employeeToPatch, ModelState);
            TryValidateModel(employeeToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Возвращает заголовки запросов
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, DELETE, PUT, PATCH");
            return Ok();
        }
    }
}

