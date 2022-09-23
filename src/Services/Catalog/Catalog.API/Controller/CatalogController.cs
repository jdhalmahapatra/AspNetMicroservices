using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {

        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(Exception));
            _logger = logger ?? throw new ArgumentNullException(nameof(Exception));
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }


        [HttpGet ("{id:length(24)}", Name ="GetProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProductById(id);
            if(product == null)
            {
                _logger.LogError($"Product with {id}, not found");
                return NotFound();
            }
            return Ok(product);
        }


        [Route("[action]/{category}", Name ="GetProdutByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductsByCategory(category);
            return Ok(products);
        }

        [Route("[action]/{name}", Name ="GetProductByName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _repository.GetProductsByName(name);
            return Ok(products);
        }



        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
           return Ok(await _repository.UpdateProduct(product));
            
        }

        [HttpDelete("{id:length(24)}", Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _repository.DeleteProdcut(id));
        }
        
    }
}

