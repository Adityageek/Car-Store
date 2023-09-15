using CarBuyer.Data;
using CarBuyer.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace CarBuyer.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class CarBuyerContoller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Car>>> SearchCars([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Car, Car>();

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            // Sort by parameters
            query = searchParams.OrderBy switch
            {
                "modelyear" => query.Sort(x => x.Ascending(y => y.ModelYear)),
                _ => query.Sort(x => x.Ascending(y => y.CreatedAt)),
            };

            // Filter by parameters
            query = searchParams.FilterBy switch
            {
                "available" => query.Match(x => x.Status == "Available"),
                "sold" => query.Match(x => x.Status == "Sold"),
                _ => query.Sort(x => x.Ascending(y => y.CreatedAt)),
            };

            if (!string.IsNullOrEmpty(searchParams.Name))
            {
                query.Match(x => x.Name == searchParams.Name);
            }

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                results = result.Results,
                totalCount = result.TotalCount
            });
        }

    }
}
