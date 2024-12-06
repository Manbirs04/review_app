using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Models;

namespace ReviewHubBackend.Data
{
    public class ReviewRepository
    {
        private readonly ReviewHubDbContext _context;

        public ReviewRepository(ReviewHubDbContext context)
        {
            _context = context;
        }

        // Retrieve all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Retrieve user by ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Retrieve all reviews
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .ToListAsync();
        }

        // Retrieve review by ID
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        // Delete a review by ID
        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        // Retrieve category by name
        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }
    }
}
