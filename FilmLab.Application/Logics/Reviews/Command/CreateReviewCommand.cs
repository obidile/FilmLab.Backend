using FilmLab.Application.Common.Interface;
using FilmLab.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmLab.Application.Logics.Reviews.Command;

public class CreateReviewCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public long MovieId { get; set; }
}
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, string>
{
    private readonly IApplicationContext _dbContext;
    public CreateReviewCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
            var movie = await _dbContext.Movies.FindAsync(request.MovieId);

            if (movie == null)
            {
                return $"Movie with Id {request.MovieId} does not exist.";
            }

            var review = new Review
            {
                Email = request.Email,
                Comment = request.Comment,
                Rating = request.Rating,
                CreatedDate = DateTime.Now,
                MovieId = request.MovieId
            };

            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Thank you for your feedback.";
    }
}