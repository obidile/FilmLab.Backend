using FilmLab.Application.Common.Interface;
using FilmLab.Application.Helpers;
using FilmLab.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Movies.Command;

public class CreateMovieCommand : IRequest<string>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public DateTime Year { get; set; }
    public string Language { get; set; }
    public IFormFile MovieUrl { get; set; }
}
public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, string>
{
    private readonly IApplicationContext _dbContext;
    private readonly IMediator _mediator;

    public CreateMovieCommandHandler(IApplicationContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<string> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exist = await _dbContext.Movies.AsNoTracking().AnyAsync(x => x.Title.ToLower() == request.Title.ToLower());
            if (exist)
            {
                return $"{request.Title.ToUpper()} already exists on this platform. Try uploading another movie.";
            }

            // Check if movie length is up to 20 minutes
            var fileInfo = new FileInfo(request.MovieUrl.FileName);
            if (fileInfo.Exists && fileInfo.Length < 20 * 60 * 1000)
            {
                return $"Movie length is less than 20 minutes. Minimum length allowed is 20 minutes.";
            }

            var model = new Movie()
            {
                Title = request.Title,
                Description = request.Description,
                Genre = request.Genre,
                Year = request.Year.Year,
                Language = request.Language,
                UploadedDate = DateTime.UtcNow
            };

            _dbContext.Movies.Add(model);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.MovieUrl != null || request.MovieUrl?.Length > 0)
            {
                string folder = $"Movies/MoviesFolder/{model.Id}";
                var fileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine($"wwwroot/{folder}", fileName);

                // Check if model.Id property is null or empty
                if (string.IsNullOrEmpty(model.Id.ToString()))
                {
                    return "Error: Invalid model.Id value";
                }

                await UploadHelper.UploadFile(request.MovieUrl, filePath);
                model.VideoUrl = $"/{folder}/{fileName}";

                _dbContext.Movies.Update(model);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return "An error occurred while uploading flag. Please try again.";
            }

            return "Movie was successfully created.";
        }
        catch (Exception)
        {
            throw;
        }
    }

}