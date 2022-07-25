using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoD.Application.Common;
using ProjetoD.Application.Contracts;
using ProjetoD.Application.Models.Dtos;
using ProjetoD.Application.Models.Request;
using ProjetoD.Application.Models.Response;
using ProjetoD.Application.Services;
using ProjetoD.Domain.Aggregate.Models;
using ProjetoD.Infra.Data;

namespace ProjetoD.Application.Service.Movieservice
{
    public class MovieService : BaseService<MovieService>, IMovieService
    {
        private readonly SqlDbContext _db;

        public MovieService(ILogger<MovieService> logger, SqlDbContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<GetAllMoviesResponse> GetAllAsync()
        {

            var response = new GetAllMoviesResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .ToListAsync();

            response = new GetAllMoviesResponse()
            {
                Movies = entity != null ? entity.
                                                Select(p => (MovieDto)p).ToList()
                                        : new List<MovieDto>()
            };

            foreach (MovieDto movie in response.Movies)
                movie.OmdbScores = await FindOmdbScores(movie.ImdbId);

            return response;
        }

        public async Task<GetByIdMovieResponse> GetByIdAsync(int id)
        {
            var response = new GetByIdMovieResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .FirstOrDefaultAsync(m => m.Id == id);

            if (entity != null)
            {
                response.MovieById = (MovieDto)entity;

                response.MovieById.OmdbScores = await FindOmdbScores(response.MovieById.ImdbId);
            }

            return response;
        }

        public async Task<GetByNameMoviesResponse> GetByNameAsync(string name)
        {
            var response = new GetByNameMoviesResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                                            .OrderBy(m => m.Name)
                                            .ToListAsync();

            response = new GetByNameMoviesResponse()
            {
                MoviesByName = entity != null ? entity.
                                                    Select(m => (MovieDto)m).ToList()
                                                : new List<MovieDto>()
            };

            foreach (MovieDto movie in response.MoviesByName)
                movie.OmdbScores = await FindOmdbScores(movie.ImdbId);

            return response;
        }

        public async Task<GetByImdbIdMovieResponse> GetByImdbIdAsync(string imdbID)
        {
            var response = new GetByImdbIdMovieResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .FirstOrDefaultAsync(m => m.ImdbId == imdbID);

            if (entity != null)
            {
                response.MovieByImdb = (MovieDto)entity;

                response.MovieByImdb.OmdbScores = await FindOmdbScores(response.MovieByImdb.ImdbId);
            }

            return response;
        }

        public async Task<GetByGenreMoviesResponse> GetByGenreAsync(int idGenre)
        {
            var response = new GetByGenreMoviesResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .Where(m => m.GenreId == idGenre)
                                            .OrderBy(m => m.Name)
                                            .ToListAsync();

            response = new GetByGenreMoviesResponse()
            {
                MoviesByGenre = entity != null ? entity.
                                                    Select(m => (MovieDto)m).ToList()
                                                : new List<MovieDto>()
            };

            foreach (MovieDto movie in response.MoviesByGenre)
                movie.OmdbScores = await FindOmdbScores(movie.ImdbId);

            return response;
        }

        public async Task<GetByWatchedMoviesResponse> GetByWatchedAsync(bool watched)
        {
            var response = new GetByWatchedMoviesResponse();

            var entity = await _db.Movie.Include(m => m.Genre)
                                            .Where(m => m.Watched == watched)
                                            .OrderBy(m => m.Name)
                                            .ToListAsync();

            response = new GetByWatchedMoviesResponse()
            {
                MoviesByWatched = entity != null ? entity.
                                                    Select(m => (MovieDto)m).ToList()
                                                : new List<MovieDto>()
            };

            foreach (MovieDto movie in response.MoviesByWatched)
                movie.OmdbScores = await FindOmdbScores(movie.ImdbId);

            return response;
        }

        public async Task<CreateMovieResponse> CreateAsync(CreateMovieRequest request)
        {
            if (request == null)
                throw new ArgumentException("Empty request!");

            var newMovie = Movie.Create(request.Name, request.ImdbId, request.Description,
                                        request.ReleaseDate, request.GenreId, request.Watched,
                                        request.UserScore);

            _db.Movie.Add(newMovie);

            await _db.SaveChangesAsync();

            return new CreateMovieResponse() { Id = newMovie.Id };
        }

        public async Task<UpdateMovieResponse> UpdateAsync(int id, UpdateMovieRequest request)
        {
            if (request == null)
                throw new ArgumentException("Empty request!");

            var entity = await _db.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (entity != null)
            {
                entity.Update(request.Name, request.ImdbId, request.Description,
                                request.ReleaseDate, request.GenreId, request.Watched,
                                request.UserScore);
                await _db.SaveChangesAsync();
            }

            return new UpdateMovieResponse();
        }

        public async Task<DeleteMovieResponse> DeleteAsync(int id)
        {
            var entity = await _db.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (entity != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return new DeleteMovieResponse();
        }

        private async Task<List<object>> FindOmdbScores(string imdbId)
        {
            List<object> omdbScores = new List<object>();
            string uri = string.Format("http://www.omdbapi.com/?apikey=fb3f36f3&i={0}", imdbId);
            string response = await APIService.ExecuteGet(uri);
            JsonArray jsonArray = JsonObject.Parse(response)["Ratings"].AsArray();

            if (jsonArray.Count < 1)
                return omdbScores;

            omdbScores = jsonArray.Select(k => new { Source = k["Source"], Value = k["Value"] }).ToList<object>();

            return omdbScores;
        }
    }
}
