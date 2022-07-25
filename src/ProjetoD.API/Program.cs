using Microsoft.EntityFrameworkCore;
using ProjetoD.Application.Contracts;
using ProjetoD.Application.Service.GenreService;
using ProjetoD.Application.Service.Movieservice;
using ProjetoD.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
// builder.Services.AddDbContext<SqlDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("AWSMSQL")));
builder.Services.AddDbContext<SqlDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoD v1"));
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    dataContext.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

