using AutoMapper;
using FilmLab.Application.Common.Interface;
using FilmLab.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Movies.Quries;

public class GetAllMoviesQuery : IRequest<List<MovieModel>>
{
    public string SearchText { get; set; }
    public int? Year { get; set; }
    public string Genre { get; set; }
    public DateTime? UploadedDate { get; set; }
}
public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<MovieModel>>
{
    private readonly IApplicationContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllMoviesQueryHandler(IApplicationContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<MovieModel>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(x => x.Title.Contains(request.SearchText));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.Year == request.Year);
        }

        if (!string.IsNullOrEmpty(request.Genre))
        {
            query = query.Where(x => x.Genre == request.Genre);
        }

        if (request.UploadedDate.HasValue)
        {
            var startDate = request.UploadedDate.Value.Date;
            var endDate = startDate.AddDays(1);

            query = query.Where(x => x.UploadedDate >= startDate && x.UploadedDate < endDate);
        }

        var movies = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<MovieModel>>(movies);
    }
}