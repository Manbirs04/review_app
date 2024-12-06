using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReviewHubBackend.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; } // Primary Key

        [Required]
        public int UserId { get; set; } // Foreign key to User table

        [ForeignKey("UserId")]
        [JsonIgnore] // Ignore to avoid serialization cycles
        public User User { get; set; } // Navigation property

        [Required]
        public int CategoryId { get; set; } // Foreign key to Category table

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } // Navigation property

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } // Rating (1 to 5)

        [Required]
        public string ReviewText { get; set; } // Review text
    }
}
