﻿using AutoMapper;
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
            var manufacturerDto = manufacturer.Select(c => new ManufacturerDto
            {
                Id = c.Id,
                NameManufacturer = c.NameManufacturer,
            }).ToList();
            return Ok(manufacturerDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetManufacturer(Guid id)
        {
            var manufacturer = _repository.Manufacturer.GetManufacturer(id, trackChanges: false);
            if (manufacturer == null)
            {
                _logger.LogInfo($"Manufacturer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var manufacturerDto = _mapper.Map<ManufacturerDto>(manufacturer);
                return Ok(manufacturerDto);
            }
        }
    }
}
