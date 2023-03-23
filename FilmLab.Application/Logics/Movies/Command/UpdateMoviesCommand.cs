using FilmLab.Application.Common.Interface;
using FilmLab.Application.Helpers;
using FilmLab.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Movies.Command;

public class UpdateMoviesCommand : IRequest<string>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public DateTime Year { get; set; }
    public string Language { get; set; }
    public IFormFile MovieUrl { get; set; }
}
public class UpdateMoviesCommandHandler : IRequestHandler<UpdateMoviesCommand, string>
{
    private readonly IApplicationContext _dbContext;
    public UpdateMoviesCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(UpdateMoviesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (movie == null)
            {
                return $"Movie with Id {request.Id} does not exist.";
            }

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.Genre = request.Genre;
            movie.Year = request.Year.Year;
            movie.Language = request.Language;

            if (request.MovieUrl != null)
            {
                string folder = $"Movies/MoviesFolder/{movie.Id}";
                var fileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine($"wwwroot/{folder}", fileName);

                await UploadHelper.UploadFile(request.MovieUrl, filePath);
                movie.VideoUrl = $"/{folder}/{fileName}";
            }

            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Movie was successfully updated.";
        }
        catch (Exception)
        {
            throw;
        }
    }
}