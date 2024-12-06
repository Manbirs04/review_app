using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ReviewHubDbContext _context;

        public CategoryController(ReviewHubDbContext context)
        {
            _context = context;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories.Count == 0)
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Category name cannot be empty.");
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllCategories), new { id = category.CategoryId }, category);
        }

        // GET: api/category/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(category);
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
