using FilmLab.Application.Common.Interface;
using FilmLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Common.Interfaces;

public class ApplicationContext : DbContext, IApplicationContext //DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {}

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
}
