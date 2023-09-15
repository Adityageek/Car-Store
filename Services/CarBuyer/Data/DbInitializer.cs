using CarBuyer.Services;
using MongoDB.Driver;
using MongoDB.Entities;

namespace CarBuyer.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings
                        .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Car>()
                .Key(x => x.Name, KeyType.Text)
                .Key(x => x.ModelYear, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Car>();

            using var scope = app.Services.CreateScope();

            var httpClient = scope.ServiceProvider.GetRequiredService<CarServiceHttpClient>();

            var cars = await httpClient.GetCarsForSearchDb();
            Console.WriteLine(cars.Count + " returned from the cars service");

            if (cars.Count > 0) await DB.SaveAsync(cars);
        }
    }
}
