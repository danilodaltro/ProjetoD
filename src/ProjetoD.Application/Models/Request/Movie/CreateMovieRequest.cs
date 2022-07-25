namespace ProjetoD.Application.Models.Request
{
    public class CreateMovieRequest
    {
        public string Name { get; set; }
        public string ImdbId { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public bool Watched { get; set; }
        public decimal? UserScore { get; set; }
    }
}
