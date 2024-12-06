using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewHubBackend.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ReviewHubDbContext _context;

        public CategoryController(ReviewHubDbContext context)
        {
            _context = context;
        }

        // Display all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories); // Pass categories to the Index view
        }

        // Display form for adding a new category
        public IActionResult Add()
        {
            return View();
        }

        // Handle form submission for adding a new category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Display details of a specific category
        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Display form for deleting a category
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Handle form submission for deleting a category
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
