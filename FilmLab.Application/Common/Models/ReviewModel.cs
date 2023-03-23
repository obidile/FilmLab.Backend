using FilmLab.Application.Mappers;
using FilmLab.Domain.Entities;

namespace FilmLab.Application.Models;

public class ReviewModel : IMapFrom<Review>
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public Movie Movie { get; set; }
    public long MovieId { get; set; }
}
