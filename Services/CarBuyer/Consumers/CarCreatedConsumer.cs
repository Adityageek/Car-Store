using AutoMapper;
using CarBuyer.Data;
using Events;
using MassTransit;
using MongoDB.Entities;

namespace CarBuyer.Consumers
{
    public class CarCreatedConsumer : IConsumer<CarCreated>
    {
        private readonly IMapper _mapper;

        public CarCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<CarCreated> carCreated)
        {
            Console.WriteLine("Consuming car created " + carCreated.Message.Id);

            var car = _mapper.Map<Car>(carCreated.Message);

            await car.SaveAsync();
        }
    }
}
