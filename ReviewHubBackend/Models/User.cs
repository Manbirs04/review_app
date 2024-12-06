using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubBackend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } // Primary Key

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Default role is "User"

        // Navigation property
        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // Initialize the collection
    }
}
