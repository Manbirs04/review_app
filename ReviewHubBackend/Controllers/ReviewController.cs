using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Data;
using ReviewHubBackend.DTOs;
using ReviewHubBackend.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReviewHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewHubDbContext _dbContext;

        public ReviewController(ReviewHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/review
        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            // Validate the category
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == reviewDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            // Validate the user
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == reviewDto.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            var review = new Review
            {
                UserId = reviewDto.UserId,
                CategoryId = reviewDto.CategoryId,
                Rating = reviewDto.Rating,
                ReviewText = reviewDto.ReviewText,
                Category = category,
                User = user
            };

            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, review);
        }

        // GET: api/review
        [HttpGet]
        public async Task<ActionResult> GetReviews()
        {
            var reviews = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .ToListAsync();

            if (!reviews.Any())
            {
                return NotFound("No reviews found.");
            }

            return Ok(reviews);
        }

        // GET: api/review/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetReviewById(int id)
        {
            var review = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            return Ok(review);
        }

        // DELETE: api/review/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            // Extract user role from claims
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || roleClaim.Value != "Admin")
            {
                return Unauthorized("Only admins can delete reviews.");
            }

            var review = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/review/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewDto)
        {
            var existingReview = await _dbContext.Reviews
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (existingReview == null)
            {
                return NotFound("Review not found.");
            }

            // Validate the category
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == reviewDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category ID");
            }

            // Validate the user
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == reviewDto.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            existingReview.CategoryId = reviewDto.CategoryId;
            existingReview.Rating = reviewDto.Rating;
            existingReview.ReviewText = reviewDto.ReviewText;
            existingReview.Category = category;
            existingReview.User = user;

            _dbContext.Reviews.Update(existingReview);
            await _dbContext.SaveChangesAsync();

            return Ok(existingReview);
        }
    }
}
