using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using ShopSmarfone.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Entities.RequestFeatures;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ShopSmarfone.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "CompanyById")]
        [HttpHead("{id}")]
        public async Task<IActionResult> GetCompanies(Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }
        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
        [HttpGet(Name = "GetCompanies"), Authorize(Roles = "User")]
        [HttpHead]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters parameters)
        {

             var companies =  await _repository.Company.GetAllCompaniesAsync(trackChanges: false, parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(companies.MetaData));
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
               return Ok(companiesDto);
            
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {   
            var companyEntity = _mapper.Map<Company>(company);
             _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task <IActionResult> DeleteCompany(Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges: false);
            
            _repository.Company.DeleteCompany(company);
           await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task <IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            var companyEntity = HttpContext.Items["company"] as Company;
            _mapper.Map(company, companyEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}


