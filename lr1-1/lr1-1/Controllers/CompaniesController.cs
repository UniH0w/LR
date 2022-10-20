using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lr1_1.Controllers
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
        [HttpGet("{id}")]
        public IActionResult GetCompanies(Guid id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);
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

            /*var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return Ok(companiesDto);*/
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}  ");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

