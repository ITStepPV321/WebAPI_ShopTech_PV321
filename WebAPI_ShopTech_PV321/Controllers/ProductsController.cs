using Microsoft.AspNetCore.Mvc;
using WebAPI_ShopTech_PV321.Core.DTOs;
using WebAPI_ShopTech_PV321.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_ShopTech_PV321.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService) {
            _productsService = productsService;        
        }
        //[C]reate
        //[R]ead
        //[U]pdate
        //[D]elete

        //[HttpGet("all")] //GET: root/api/products/all
        //[HttpGet("/all")] //GET: root/all
        // GET: api/products
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productsService.GetAll();
            return Ok(products); //200
                
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto product)
        {
            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDto product)
        {
            return Ok();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
