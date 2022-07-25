using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByWatchedMoviesResponse: BaseResponse
    {
        public List<MovieDto> MoviesByWatched { get; set; }
    }
}