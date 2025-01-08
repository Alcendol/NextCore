using Microsoft.AspNetCore.Mvc;

namespace NextCore.backend.WebAPI.Controller.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Product 1" },
                new { Id = 2, Name = "Product 2" },
                new { Id = 3, Name = "Product 3" }
            };
            return Ok(products);
        }
    }
}
