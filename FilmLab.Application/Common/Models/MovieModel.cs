using FilmLab.Application.Mappers;
using FilmLab.Domain.Entities;

namespace FilmLab.Application.Models;

public class MovieModel : IMapFrom<Movie>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public string Language { get; set; }
    public string VideoUrl { get; set; }
    public DateTime UploadedDate { get; set; }
    public List<Review> Reviews { get; set; }
}
