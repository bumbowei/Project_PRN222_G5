using Project_PRN222_G5.DataAccess.Entities.Bookings;
using Project_PRN222_G5.DataAccess.Entities.Cinemas;
using Project_PRN222_G5.DataAccess.Entities.Common;

namespace Project_PRN222_G5.DataAccess.Entities.Movies;

public class Showtime : BaseEntity<int>
{
    public Guid MovieId { get; set; } = Guid.Empty;
    public Movie Movie { get; set; } = null!;
    public Guid RoomId { get; set; } = Guid.Empty!;
    public Room Room { get; set; } = null!;
    public DateTimeOffset StartTime { get; set; } = default;
    public decimal Price { get; set; } = default;
    public ICollection<Booking> Bookings { get; set; } = [];
}