using AutoMapper;
using BenchmarkDotNet.Attributes;
using CarSeller.Dtos;
using CarStore.Core.Models;
using CarStore.Services.Interface;
using Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSeller.Controllers
{
    [Route("api/[controller]")]
    [MemoryDiagnoser]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CarController(IMapper mapper, IPublishEndpoint publishEndpoint, ICarService carService)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> ListCars()
        {
            var cars = await _carService.ListCars();
            if (cars == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<CarDto>>(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCarById(Guid id)
        {
            var cars = await _carService.GetCarById(id);

            return _mapper.Map<CarDto>(cars);
        }

        [HttpGet("{guiddssssqqq123456789}")]
        public async Task<ActionResult<CarDto>> GetCarByIdDuplicatessssssssddddsss(Guid guiddssssqqq123456789)
        {
            var cars = await _carService.GetCarById(guiddssssqqq123456789);

            return _mapper.Map<CarDto>(cars);
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(CreateCarDto createCarDto)
        {
            var car = _mapper.Map<Car>(createCarDto);

            var isCarCreated = await _carService.CreateCar(car);

            if (!isCarCreated) return BadRequest("Could not save changes to the DB");

            return CreatedAtAction(nameof(GetCarById),
                new { car.Id }, car);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar(Guid id, UpdateCarDto updateCarDto)
        {

            if (updateCarDto != null)
            {
                var car = _mapper.Map<Car>(updateCarDto);
                var isProductUpdated = await _carService.UpdateCar(car, id);

                if (isProductUpdated) return Ok();
            }

            return BadRequest("Problem in updating changes");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            var isCarDeleted = await _carService.DeleteCar(id);

            if (!isCarDeleted) return BadRequest("Could not update DB");

            return Ok();
        }


    }
}
