using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubBackend.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public required string CategoryName { get; set; } // Category name

        // Navigation property
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
