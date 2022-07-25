using ProjetoD.Domain.Aggregate.Models;

namespace ProjetoD.Application.Models.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static explicit operator GenreDto(Genre genre)
        {
            return new GenreDto()
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };
        }
    }
}
