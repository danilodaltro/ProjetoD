using System.Collections.Generic;
using ProjetoD.Application.Common;
using ProjetoD.Application.Models.Dtos;

namespace ProjetoD.Application.Models.Response
{
    public class GetAllGenresResponse: BaseResponse
    {
        public List<GenreDto> Genres { get; set; }
    }
}
