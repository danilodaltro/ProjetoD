using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByIdMovieResponse : BaseResponse
    {
        public MovieDto MovieById { get; set; }
    }
}
