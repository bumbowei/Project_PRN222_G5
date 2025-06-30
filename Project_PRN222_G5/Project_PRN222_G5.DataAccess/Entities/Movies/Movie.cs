using Project_PRN222_G5.DataAccess.Entities.Common;
using Project_PRN222_G5.DataAccess.Entities.Movies.Enum;

namespace Project_PRN222_G5.DataAccess.Entities.Movies;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Genre? Genre { get; set; } = default;
    public int? Duration { get; set; } = default;
    public string? PosterPath { get; set; } = string.Empty;
    public MovieStatus Status { get; set; } = MovieStatus.Active;
    public ICollection<Showtime> Showtimes { get; set; } = [];
}