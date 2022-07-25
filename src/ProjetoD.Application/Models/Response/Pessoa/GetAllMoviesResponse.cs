using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetAllMoviesResponse: BaseResponse
    {
        public List<MovieDto> Movies { get; set; }
    }
}
