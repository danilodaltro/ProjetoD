using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByIdGenreResponse : BaseResponse
    {
        public GenreDto GenreById { get; set; }
    }
}
