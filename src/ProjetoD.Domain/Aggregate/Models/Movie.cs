using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoD.Domain.Aggregate.Models
{
    public sealed class Movie
    {

        public Movie() { }

        private Movie(string name, string imdbId, string description, DateTime releaseDate,
                        int genreId, bool watched, decimal? userScore)
        {
            this.Name = name;
            this.ImdbId = imdbId;
            this.Description = description;
            this.ReleaseDate = releaseDate;
            this.GenreId = genreId;
            this.Watched = watched;
            this.UserScore = userScore;
        }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }

        [NotMapped]
        public Genre Genre { get; set; }

        public bool Watched { get; set; }

        public decimal? UserScore { get; set; }

        public static Movie Create(string name, string imdbId, string description, DateTime releaseDate,
                                    int genreId, bool watched, decimal? userScore)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid " + nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Invalid " + nameof(description));

            if (genreId == 0)
                throw new ArgumentException("Invalid genre");

            return new Movie(name, imdbId, description, releaseDate, genreId, watched, userScore);
        }

        public void Update(string name, string imdbId, string description, DateTime? releaseDate,
                            int genreId, bool? watched, decimal? userScore)
        {
            bool isNameNullOrWhiteSpace = string.IsNullOrWhiteSpace(name);
            bool isImdbIdNullOrWhiteSpace = string.IsNullOrWhiteSpace(imdbId);
            bool isDescriptionNullOrWhiteSpace = string.IsNullOrWhiteSpace(description);

            if (!isNameNullOrWhiteSpace)
                this.Name = name;

            if (!isImdbIdNullOrWhiteSpace)
                this.ImdbId = imdbId;

            if (!isDescriptionNullOrWhiteSpace)
                this.Description = description;

            if (releaseDate.HasValue)
                this.ReleaseDate = releaseDate.Value;

            if (genreId > 0)
                this.GenreId = genreId;

            if(watched.HasValue)
                this.Watched = watched.Value;

            if(userScore.HasValue)
                this.UserScore = userScore.Value;
        }
    }
}
