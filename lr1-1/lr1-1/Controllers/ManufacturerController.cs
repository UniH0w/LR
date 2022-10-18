using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr1_1.Controllers
{
    [Route("api/manufacturer")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ManufacturerController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;


        }
        [HttpGet]
        public IActionResult GetManufacturer()
        {

            var manufacturer = _repository.Manufacturer.GetAllManufacturer(trackChanges: false);
            var manufacturerDto = _mapper.Map<IEnumerable<ManufacturerDto>>(manufacturer);
            return Ok(manufacturerDto);

        }
    }
}
