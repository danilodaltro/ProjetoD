using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByImdbIdMovieResponse : BaseResponse
    {
        public MovieDto MovieByImdb { get; set; }
    }
}