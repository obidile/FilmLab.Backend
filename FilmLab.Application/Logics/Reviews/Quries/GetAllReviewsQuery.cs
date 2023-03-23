using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmLab.Application.Common.Interface;
using FilmLab.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Reviews.Quries;

public class GetAllReviewsQuery : IRequest<string>
{
    public DateTime CreatedDate { get; set; }
}
public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, string>
{
    private readonly IApplicationContext _dbContext;
    private readonly IMapper _mapper;
    public GetAllReviewsQueryHandler(IApplicationContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<string> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Reviews.AsNoTracking().OrderBy(x => x.CreatedDate).ProjectTo<ReviewModel>(_mapper.ConfigurationProvider).ToListAsync();

        return query.ToString();
    }
}