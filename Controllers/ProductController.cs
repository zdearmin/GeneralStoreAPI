using System.Security.Cryptography.X509Certificates;
using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        private GeneralStoreDBContext _db;
        public ProductController(GeneralStoreDBContext db) 
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductEdit newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = new Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                QuantityInStock = newProduct.Quantity,
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() 
        {
            var products = await _db.Products.ToListAsync();
            return Ok(products);
        }

        // [HttpGet]
        // [Route("{id}")]
        // public async Task<IActionResult> GetProductById(int id)
        // {
        //     var product = await _db.Products.FindAsync(id);
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(product);
        // }

        // [HttpPut]
        // [Route("{id}")]
        // public async Task<IActionResult> UpdateProduct([FromForm] ProductEdit model, [FromRoute] int id)
        // {
        //     var oldProduct = await _db.Products.FindAsync(id);
        //     if (oldProduct == null)
        //     {
        //         return NotFound();
        //     }
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }
        //     if (!string.IsNullOrEmpty(model.Name))
        //     {
        //         oldProduct.Name = model.Name;
        //     }
        //     if (!int.Equals(model.Quantity) null)
        //     {
        //         return NotFound();
        //     }
        //     if (!float(model.Price) == null)
        //     {
        //         return NotFound();
        //     }
        // }
    }
}