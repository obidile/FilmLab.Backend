using AutoMapper;
using FilmLab.Application.Common.Interface;
using FilmLab.Domain.Entities;
using MediatR;

namespace FilmLab.Application.Logics.Movies.Quries;

public class GetMovieByIdQuery : IRequest<string>
{
    public GetMovieByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}
public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, string>
{
    private readonly IApplicationContext _dbContext;
    private readonly IMapper _mapper;
    public GetMovieByIdQueryHandler(IApplicationContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<string> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _dbContext.Movies.FindAsync(request.Id);

        if (movie == null)
        {
            return $"Movie with {request.Id} was not found";
        }

        return movie.ToString();
    }
}