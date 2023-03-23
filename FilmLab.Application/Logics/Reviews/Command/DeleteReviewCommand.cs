using FilmLab.Application.Common.Interface;
using MediatR;

namespace FilmLab.Application.Logics.Reviews.Command;

public class DeleteReviewCommand : IRequest<string>
{
    public long Id { get; set; }
}
public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, string>
{
    private readonly IApplicationContext _dbContext;
    public DeleteReviewCommandHandler(IApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var model = await _dbContext.Reviews.FindAsync(request.Id);

        if (model == null)
        {
            return "Your feedback Id was not found";
        }

        _dbContext.Reviews.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return "Your feed back has been deleted.";
    }
}