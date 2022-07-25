using ProjetoD.Application.Models.Request;
using ProjetoD.Application.Models.Response;

namespace ProjetoD.Application.Contracts
{
    public interface IMovieService
    {
        Task<GetAllMoviesResponse> GetAllAsync();
        Task<GetByIdMovieResponse> GetByIdAsync(int id);
        Task<GetByNameMoviesResponse> GetByNameAsync(string name);
        Task<GetByImdbIdMovieResponse> GetByImdbIdAsync(string imdbID);
        Task<GetByGenreMoviesResponse> GetByGenreAsync(int idGenre);
        Task<GetByWatchedMoviesResponse> GetByWatchedAsync(bool watched);
        Task<CreateMovieResponse> CreateAsync(CreateMovieRequest request);
        Task<UpdateMovieResponse> UpdateAsync(int id, UpdateMovieRequest request);
        Task<DeleteMovieResponse> DeleteAsync(int id);
    }
}
