using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [ApiController]
    [Route("restaurants")]
    public class RestaurantsController : ControllerBase {
        [HttpGet("by-postcode/{postcode}")]
        public IActionResult Get(string postcode) {
            var response = new {
                restaurants = new[] {
                    new {
                        name = "Spicy Restaurant",
                        ratingStars = "4.77",
                        cuisineTypes = new[] {
                            new { name = "any name" },
                        },
                    },
                },
            };

            return Ok(response);
        }
    }
}