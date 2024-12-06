using Microsoft.AspNetCore.Mvc;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;
using System.Threading.Tasks;

namespace ReviewHubBackend.Controllers
{
    public class AdminController : Controller
    {
        private readonly ReviewRepository _repository;

        public AdminController(ReviewRepository repository)
        {
            _repository = repository;
        }

        // Display all users
        public async Task<IActionResult> Users()
        {
            var users = await _repository.GetAllUsersAsync();
            return View(users); // Pass users to the Users view
        }

        // Display all reviews
        public async Task<IActionResult> Reviews()
        {
            var reviews = await _repository.GetAllReviewsAsync();
            return View(reviews); // Pass reviews to the Reviews view
        }

        // Confirm delete user
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // Handle delete user
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var success = await _repository.DeleteUserAsync(id);
            if (!success) return NotFound();

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction(nameof(Users));
        }

        // Confirm delete review
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _repository.GetReviewByIdAsync(id);
            if (review == null) return NotFound();

            return View(review);
        }

        // Handle delete review
        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            var success = await _repository.DeleteReviewAsync(id);
            if (!success) return NotFound();

            TempData["Success"] = "Review deleted successfully!";
            return RedirectToAction(nameof(Reviews));
        }
    }
}
