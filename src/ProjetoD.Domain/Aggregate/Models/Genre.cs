namespace ProjetoD.Domain.Aggregate.Models
{
    public sealed class Genre
    {
        public Genre() { }

        private Genre(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
        
        public static Genre Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid " + nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Invalid " + nameof(description));

            return new Genre(name, description);
        }

        public void Update(string name, string description)
        {
            bool isNameNullOrWhiteSpace = string.IsNullOrWhiteSpace(name);
            bool isDescriptionNullOrWhiteSpace = string.IsNullOrWhiteSpace(description);

            if(!isNameNullOrWhiteSpace)
                this.Name = name;

            if(!isDescriptionNullOrWhiteSpace)
                this.Description = description;
        }
    }
}