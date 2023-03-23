using FilmLab.Application.Common.Helpers;
using FilmLab.Application.Common.Interface;
using FilmLab.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FilmLab.Application.Logics.Movies.Quries;

public class DownloadMovieQuery : IRequest<byte[]>
{
    public int Id { get; set; }
}
public class DownloadMovieQueryHandler : IRequestHandler<DownloadMovieQuery, byte[]>
{
    private readonly IApplicationContext _dbContext;

    public DownloadMovieQueryHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<byte[]> Handle(DownloadMovieQuery request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (movie == null)
        {
            return Encoding.UTF8.GetBytes($"Movie with Id = {request.Id} was not found.");
        }

        var fileData = await DownloadHelper.DownloadFile(movie.VideoUrl);
        if (fileData == null)
        {
            return Encoding.UTF8.GetBytes("File not found");
        }

        return fileData;
    }

}