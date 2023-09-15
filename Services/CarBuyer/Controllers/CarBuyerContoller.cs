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

            if (!string.IsNullOrEmpty(searchParams.Name))
            {
                query.Match(Search.Full, searchParams.Name).SortByTextScore();
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
