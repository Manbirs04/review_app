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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        internal async Task GetCategoryByNameAsync(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
