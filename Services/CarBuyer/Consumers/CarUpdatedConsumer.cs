using AutoMapper;
using CarBuyer.Data;
using Events;
using MassTransit;
using MongoDB.Entities;

namespace CarBuyer.Consumers
{
    public class CarUpdatedConsumer : IConsumer<CarUpdated>
    {
        private readonly IMapper _mapper;

        public CarUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CarUpdated> carUpdated)
        {
            Console.WriteLine("Consuming car update " + carUpdated.Message.Id);

            var car = _mapper.Map<Car>(carUpdated.Message);

            var result = await DB.Update<Car>().Match(car => car.ID == carUpdated.Message.Id).ModifyOnly(
                car => new
                {
                    car.Name,
                    car.ModelYear,
                    car.Description,
                    car.Price,
                    car.Kilometeres,
                    car.Image,
                    car.Color,
                    car.UpdatedAt,

                }, car).ExecuteAsync();
            if (!result.IsAcknowledged)
                throw new MessageException(typeof(CarUpdated), "Problem updating mongodb");
        }
    }
}
