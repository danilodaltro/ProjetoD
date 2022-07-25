using ProjetoD.Application.Models.Request;
using ProjetoD.Application.Models.Response;

namespace ProjetoD.Application.Contracts
{
    public interface IGenreService
    {
        Task<GetAllGenresResponse> GetAllAsync();
        Task<GetByIdGenreResponse> GetByIdAsync(int id);
        Task<GetByNameGenresResponse> GetByNameAsync(string name);
        Task<CreateGenreResponse> CreateAsync(CreateGenreRequest request);
        Task<UpdateGenreResponse> UpdateAsync(int id, UpdateGenreRequest request);
        Task<DeleteGenreResponse> DeleteAsync(int id);
    }
}