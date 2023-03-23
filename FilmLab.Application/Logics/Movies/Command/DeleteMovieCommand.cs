using FilmLab.Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Movies.Command;

public class DeleteMovieCommand : IRequest<string>
{
    public long Id { get; set; }
}
public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, string>
{
    private readonly IApplicationContext _dbContext;
    public DeleteMovieCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var model = await _dbContext.Movies.FindAsync(request.Id);

        if (model == null)
        {
            return "School was not found";
        }

        _dbContext.Movies.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return "Movie was successfully deleted";
    }
}