namespace ReviewHubBackend.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int Rating { get; set; }
        public required string ReviewText { get; set; }
    }

}
