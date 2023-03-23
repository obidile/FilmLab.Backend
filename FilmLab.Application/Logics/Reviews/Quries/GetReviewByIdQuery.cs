using FilmLab.Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Reviews.Quries;

public class GetReviewByIdQuery : IRequest<string>
{
    public GetReviewByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}
public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, string>
{
    private readonly IApplicationContext _dbContext;
    public GetReviewByIdQueryHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (review == null)
        {
            return $"Review {request.Id} not found";
        }

        return review.ToString();
    }
}