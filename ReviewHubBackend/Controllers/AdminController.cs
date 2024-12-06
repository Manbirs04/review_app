using Microsoft.AspNetCore.Mvc;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;

namespace ReviewHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ReviewRepository _repository;

        public AdminController(ReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _repository.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _repository.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpDelete("reviews/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await _repository.DeleteReviewAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
