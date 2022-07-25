

using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetByNameGenresResponse: BaseResponse
    {
        public List<GenreDto> GenresByName { get; set; }
    }
}