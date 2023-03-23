namespace FilmLab.Domain.Entities;

public class Review
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public Movie Movie { get; set; }
    public long MovieId { get; set; }
}
