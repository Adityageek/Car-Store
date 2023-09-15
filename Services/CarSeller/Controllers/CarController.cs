using AutoMapper;
using CarSeller.Data;
using CarSeller.Dtos;
using CarSeller.Entities;
using CarSeller.Helpers;
using Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSeller.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly CarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CarController(CarDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> ListCars()
        {
            var cars = await _context.Cars.OrderBy(x => x.UpdatedAt).ToListAsync();

            return _mapper.Map<List<CarDto>>(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCarById(Guid id)
        {
            var cars = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CarDto>(cars);
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(CreateCarDto createCarDto)
        {
            var car = _mapper.Map<Car>(createCarDto);

            _context.Cars.Add(car);

            var newCar = _mapper.Map<CarDto>(car);

            await _publishEndpoint.Publish(_mapper.Map<CarCreated>(newCar));

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Could not save changes to the DB");

            return CreatedAtAction(nameof(GetCarById),
                new { car.Id }, newCar);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar(Guid id, UpdateCarDto updateCarDto)
        {

            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null) return NotFound();

            car.Description = updateCarDto.Description ?? car.Description;
            car.Name = updateCarDto.Name ?? car.Name;
            car.Status = updateCarDto.Status != null ? EnumHelper.EnumParse(updateCarDto.Status, car.Status) : car.Status;
            car.Kilometeres = updateCarDto.Kilometeres == 0 ? car.Kilometeres : updateCarDto.Kilometeres;
            car.Price = updateCarDto.Price == 0 ? car.Price : updateCarDto.Price;
            car.ModelYear = updateCarDto.ModelYear == 0 ? car.ModelYear : updateCarDto.ModelYear;
            car.Color = updateCarDto.Color ?? car.Color;
            car.Image = updateCarDto.Image ?? car.Image;
            car.UpdatedAt = DateTime.Now;

            await _publishEndpoint.Publish(_mapper.Map<CarUpdated>(car));

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest("Problem saving changes");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null) return NotFound();

            _context.Cars.Remove(car);

            await _publishEndpoint.Publish<CarDeleted>(new { Id = car.Id.ToString() });

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Could not update DB");

            return Ok();
        }


    }
}
