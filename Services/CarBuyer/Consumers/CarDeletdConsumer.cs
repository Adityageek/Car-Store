using CarBuyer.Data;
using Events;
using MassTransit;
using MongoDB.Entities;

namespace CarBuyer.Consumers
{
    public class CarDeletdConsumer : IConsumer<CarDeleted>
    {
        public async Task Consume(ConsumeContext<CarDeleted> carDeleted)
        {
            Console.WriteLine("Consuming car delete " + carDeleted.Message.Id);

            var result = await DB.DeleteAsync<Car>(carDeleted.Message.Id);

            if (!result.IsAcknowledged)
                throw new MessageException(typeof(CarDeleted), "Problem deleting car");
        }
    }
}
