using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByNameMoviesResponse: BaseResponse
    {
        public List<MovieDto> MoviesByName { get; set; }
    }
}