using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoD.Domain.Aggregate.Models;

namespace ProjetoD.Infra.Data
{
    /// <summary>
    /// Referência de artigo para conseguir criar modelos de configuração
    /// https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
    /// Rererência de artigo para conseguir criar migration a partir de dominios
    /// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
    /// </summary>
    public class SqlDbContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public SqlDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new MovieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenreEntityTypeConfiguration());
            modelBuilder.Entity<Movie>();
            modelBuilder.Entity<Genre>();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> orderConfiguration)
        {
            orderConfiguration.ToTable("Movie", "dbo");

            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Property(o => o.Id).UseIdentityColumn();
            orderConfiguration.Property(o => o.Name).IsRequired();
            orderConfiguration.Property(o => o.ImdbId).IsRequired();
            orderConfiguration.Property(o => o.Description).IsRequired();
            orderConfiguration.Property(o => o.GenreId).IsRequired().HasColumnName("Id_Genre");
            orderConfiguration.Property(o => o.ReleaseDate).IsRequired();
            orderConfiguration.Property(o => o.Watched).IsRequired();
            orderConfiguration.Property(o => o.UserScore).HasPrecision(2);
        }
    }

    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> orderConfiguration)
        {
            orderConfiguration.ToTable("Genre", "dbo");

            orderConfiguration.HasKey(o => o.Id);
            orderConfiguration.Property(o => o.Id).UseIdentityColumn();
            orderConfiguration.Property(o => o.Name).IsRequired();
            orderConfiguration.Property(o => o.Description).IsRequired();

            orderConfiguration.HasMany(c => c.Movies).WithOne(p => p.Genre).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
