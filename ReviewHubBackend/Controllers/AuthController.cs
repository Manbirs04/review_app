using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewHubBackend.Data;
using ReviewHubBackend.Models;

namespace ReviewHubBackend.Controllers
{
    public class AuthController : Controller
    {
        private readonly ReviewHubDbContext _context;

        public AuthController(ReviewHubDbContext context)
        {
            _context = context;
        }

        // Display the Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("", "A user with this email already exists.");
                return View(user);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // Secure password hashing
            user.Role = "User"; // Default role

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }

        // Display the Login Page
        public IActionResult Login()
        {
            return View();
        }

        // Handle Login Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View();
            }

            TempData["Success"] = $"Welcome, {user.Username}!";
            // Redirect to a secure area or dashboard
            return RedirectToAction("Index", "Home");
        }
    }
}
