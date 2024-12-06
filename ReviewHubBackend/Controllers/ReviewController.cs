using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;
using System.Threading.Tasks;

namespace ReviewHubBackend.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewHubDbContext _dbContext;

        public ReviewController(ReviewHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Display all reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .ToListAsync();

            return View(reviews); // Pass the reviews to the Index view
        }

        // Display form for adding a new review
        public IActionResult Add()
        {
            ViewBag.Categories = _dbContext.Categories.ToList(); // Load categories for the dropdown
            ViewBag.Users = _dbContext.Users.ToList(); // Load users for the dropdown
            return View();
        }

        // Handle form submission for adding a review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Review review)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Reviews.Add(review);
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Review added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Users = _dbContext.Users.ToList();
            return View(review);
        }

        // Display details of a specific review
        public async Task<IActionResult> Details(int id)
        {
            var review = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // Display form for editing a review
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Users = _dbContext.Users.ToList();
            return View(review);
        }

        // Handle form submission for updating a review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reviews.Update(review);
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Review updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Users = _dbContext.Users.ToList();
            return View(review);
        }

        // Display confirmation page for deleting a review
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // Handle form submission for deleting a review
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review != null)
            {
                _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "Review deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
