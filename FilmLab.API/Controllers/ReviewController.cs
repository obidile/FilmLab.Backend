using FilmLab.Application.Logics.Movies.Command;
using FilmLab.Application.Logics.Reviews.Command;
using FilmLab.Application.Logics.Reviews.Quries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmLab.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IMediator mediator;
	public ReviewController(IMediator mediator)
	{
		this.mediator = mediator;
	}

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllReviews()
    {
        var query = new GetAllReviewsQuery();
        var orders = await mediator.Send(query);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetReviewById(long id)
    {
        var query = new GetReviewByIdQuery(id);
        var order = await mediator.Send(query);
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long Id)
    {
        await mediator.Send(new DeleteReviewCommand { Id = Id });

        return Ok("Removed Successfully");
    }
}
