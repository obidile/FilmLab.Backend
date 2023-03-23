using FilmLab.Application.Logics.Movies.Command;
using FilmLab.Application.Logics.Movies.Quries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmLab.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator mediator;
	public MoviesController(IMediator mediator)
	{
		this.mediator = mediator;
	}

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromForm] CreateMovieCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Updatemovie([FromRoute] long Id, [FromForm] UpdateMoviesCommand command)
    {
        if (command != null)
        {
            command.Id = Id;
        }
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadMovie(int id)
    {
        var fileData = await mediator.Send(new DownloadMovieQuery { Id = id });
        return File(fileData, "application/octet-stream");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetMovieByIdQuery(id)));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllMovies([FromQuery] GetAllMoviesQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long Id)
    {
        await mediator.Send(new DeleteMovieCommand { Id = Id });

        return Ok("Removed Successfully");
    }
}
