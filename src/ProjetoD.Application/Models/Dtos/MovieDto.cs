using ProjetoD.Domain.Aggregate.Models;

namespace ProjetoD.Application.Models.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImdbId { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GenreDto Genre { get; set; }
        public bool Watched { get; set; }
        public decimal? UserScore { get; set; }
        public List<object> OmdbScores { get; set; }

        public static explicit operator MovieDto(Movie movie)
        {
            return new MovieDto()
            {
                Id = movie.Id,
                Name = movie.Name,
                ImdbId = movie.ImdbId,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Genre = (GenreDto)movie.Genre,
                Watched = movie.Watched,
                UserScore = movie.UserScore
            };
        }
    }
}
