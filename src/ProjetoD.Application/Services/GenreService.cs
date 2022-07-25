using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjetoD.Application.Common;
using ProjetoD.Application.Contracts;
using ProjetoD.Application.Models.Dtos;
using ProjetoD.Application.Models.Request;
using ProjetoD.Application.Models.Response;
using ProjetoD.Domain.Aggregate.Models;
using ProjetoD.Infra.Data;

namespace ProjetoD.Application.Service.GenreService
{
    public class GenreService : BaseService<GenreService>, IGenreService
    {
        private readonly SqlDbContext _db;

        public GenreService(ILogger<GenreService> logger, SqlDbContext db) : base(logger)
        {
            _db = db;
        }

        public async Task<CreateGenreResponse> CreateAsync(CreateGenreRequest request)
        {
            if (request == null)
                throw new ArgumentException("Empty request!");

            var newGenre = Genre.Create(request.Name, request.Description);

            _db.Genre.Add(newGenre);

            await _db.SaveChangesAsync();

            return new CreateGenreResponse() { Id = newGenre.Id };
        }

        public async Task<UpdateGenreResponse> UpdateAsync(int id, UpdateGenreRequest request)
        {
            if (request == null)
                throw new ArgumentException("Empty request!");

            var entity = await _db.Genre.FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.Update(request.Name, request.Description);
                await _db.SaveChangesAsync();
            }

            return new UpdateGenreResponse();
        }

        public async Task<DeleteGenreResponse> DeleteAsync(int id)
        {
            var entity = await _db.Genre.FirstOrDefaultAsync(c => c.Id == id);

            var pessoaEntity = await _db.Movie.FirstOrDefaultAsync(p => p.GenreId == id);

            if (pessoaEntity != null)
                return new DeleteGenreResponse()
                {
                    Success = false,
                    Error = "Cannot delete genre while related with movies."
                };

            if (entity != null)
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }

            return new DeleteGenreResponse();
        }

        public async Task<GetAllGenresResponse> GetAllAsync()
        {
            var entity = await _db.Genre.ToListAsync();

            return new GetAllGenresResponse()
            {
                Genres = entity != null ? entity.
                                                Select(c => (GenreDto)c).ToList()
                                        : new List<GenreDto>()
            };
        }

        public async Task<GetByIdGenreResponse> GetByIdAsync(int id)
        {
            var response = new GetByIdGenreResponse();

            var entity = await _db.Genre.FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null) response.GenreById = (GenreDto)entity;

            return response;
        }

        public async Task<GetByNameGenresResponse> GetByNameAsync(string name)
        {
            var response = new GetByNameGenresResponse();

            var entity = await _db.Genre.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                                            .OrderBy(p => p.Name)
                                            .ToListAsync();

            return new GetByNameGenresResponse()
            {
                GenresByName = entity != null ? entity.
                                                    Select(c => (GenreDto)c).ToList()
                                                : new List<GenreDto>()
            };
        }
    }
}
