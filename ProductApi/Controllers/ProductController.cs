using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Model;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public ProductController(ProductDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        [HttpGet("GetProductByID/{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            var ObjProduct = await _context.Product.FindAsync(id);

            if (ObjProduct == null)
            {
                return NotFound();
            }

            return ObjProduct;
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product ObjProduct)
        {
            if (id != ObjProduct.ID)
            {
                return BadRequest();
            }

            _context.Entry(ObjProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("SaveProduct")]
        public async Task<ActionResult<Product>> SaveProduct(Product ObjProduct)
        {
            _context.Product.Add(ObjProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = ObjProduct.ID }, ObjProduct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var ObjProduct = await _context.Product.FindAsync(id);
            if (ObjProduct == null)
            {
                return NotFound();
            }

            _context.Product.Remove(ObjProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool InspectionExists(int id)
        {
            return _context.Product.Any(e => e.ID == id);
        }

    }
}
