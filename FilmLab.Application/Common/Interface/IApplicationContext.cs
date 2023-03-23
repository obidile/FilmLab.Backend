using FilmLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Common.Interface;

public interface IApplicationContext 
{
    DbSet<Movie> Movies { get; set; }
    DbSet<Review> Reviews { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
