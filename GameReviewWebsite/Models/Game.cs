﻿namespace GameReviewWebsite.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }

    }
}