using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByGenreMoviesResponse : BaseResponse
    {
        public List<MovieDto> MoviesByGenre{ get; set; }
    }
}