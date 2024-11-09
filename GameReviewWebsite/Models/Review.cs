namespace GameReviewWebsite.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int GameId { get; set; } 
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; }

    }
}
