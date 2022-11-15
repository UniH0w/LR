using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


namespace ShopSmarfone.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies =  await _repository.Company.GetAllCompaniesAsync(trackChanges: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}  ");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public  async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }
            var companyEntity = _mapper.Map<Company>(company);
             _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteCompany(Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Company.DeleteCompany(company);
           await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }
            var companyEntity = await _repository.Company.GetCompanyAsync(id, trackChanges: true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(company, companyEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}


