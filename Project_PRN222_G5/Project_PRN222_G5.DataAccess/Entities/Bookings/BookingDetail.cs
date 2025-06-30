using Project_PRN222_G5.DataAccess.Entities.Cinemas;

namespace Project_PRN222_G5.DataAccess.Entities.Bookings;

public class BookingDetail
{
    public Guid BookingId { get; set; }= Guid.Empty;
    public Booking Booking { get; set; } = default!;
    public Guid SeatId { get; set; } = Guid.Empty;
    public Seat Seat { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}