using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project_PRN222_G5.DataAccess.Entities.Bookings;
using Project_PRN222_G5.DataAccess.Entities.Bookings.Enum;
using Project_PRN222_G5.DataAccess.Entities.Cinemas;
using Project_PRN222_G5.DataAccess.Entities.Movies;
using Project_PRN222_G5.DataAccess.Entities.Movies.Enum;
using Project_PRN222_G5.DataAccess.Entities.Users;
using Project_PRN222_G5.DataAccess.Entities.Users.Enum;
using Project_PRN222_G5.DataAccess.Interfaces.UnitOfWork;

namespace Project_PRN222_G5.DataAccess.Data
{
    public static class DatabaseSeeder
    {
        private static readonly DateTimeOffset SeedDate = new(2025, 6, 1, 0, 0, 0, TimeSpan.FromHours(7));

        public static async Task SeedDataAsync(IServiceProvider serviceProvider, ILogger logger)
        {
            using var scope = serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await SeedUsers(unitOfWork, logger);
            await SeedCinemas(unitOfWork, logger);
            await SeedRooms(unitOfWork, logger);
            await SeedSeats(unitOfWork, logger);
            await SeedMovies(unitOfWork, logger);
            await SeedShowtimes(unitOfWork, logger);
            await SeedBookings(unitOfWork, logger);
            await SeedBookingDetails(unitOfWork, logger);
        }

        private static async Task SeedUsers(IUnitOfWork unitOfWork, ILogger logger)
        {
            var userRepo = unitOfWork.Repository<User>();
            var anyUser = await userRepo.AnyAsync();
            if (!anyUser)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                await userRepo.AddAsync(new User
                {
                    Id = adminId,
                    FullName = "Admin User",
                    Username = "admin",
                    PasswordHash = "$2b$10$2ZgkaUmY6vSIXTFKg7fpkewjufMZkj2brKEmxNyRFhPv3Ih7bOEte",
                    Email = "admin@example.com",
                    PhoneNumber = "0123456789",
                    DayOfBirth = new DateTime(1990, 1, 1),
                    Gender = Gender.Male,
                    Avatar = "/images/default-avatar.jpg",
                    UserStatus = UserStatus.Active,
                    Role = Role.Admin,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await userRepo.AddAsync(new User
                {
                    Id = userId,
                    FullName = "John Doe",
                    Username = "johndoe",
                    PasswordHash = "$2b$10$2ZgkaUmY6vSIXTFKg7fpkewjufMZkj2brKEmxNyRFhPv3Ih7bOEte",
                    Email = "john.doe@example.com",
                    PhoneNumber = "0987654321",
                    DayOfBirth = new DateTime(1995, 5, 15),
                    Gender = Gender.Male,
                    Avatar = "/images/default-avatar.jpg",
                    UserStatus = UserStatus.Active,
                    Role = Role.Customer,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial user data.");
            }
            else
            {
                logger.LogInformation("User data already exists. Skipping seed.");
            }
        }

        private static async Task SeedCinemas(IUnitOfWork unitOfWork, ILogger logger)
        {
            var cinemaRepo = unitOfWork.Repository<Cinema>();
            var anyCinema = await cinemaRepo.AnyAsync();
            if (!anyCinema)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");

                await cinemaRepo.AddAsync(new Cinema
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "Galaxy Cinema",
                    Address = "123 Main Street, Haboi",
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await cinemaRepo.AddAsync(new Cinema
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Name = "CGV Cinema",
                    Address = "456 Oak Avenue, Ho Tri Linh City",
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial cinema data.");
            }
            else
            {
                logger.LogInformation("Cinema data already exists. Skipping seed.");
            }
        }

        private static async Task SeedRooms(IUnitOfWork unitOfWork, ILogger logger)
        {
            var roomRepo = unitOfWork.Repository<Room>();
            var anyRoom = await roomRepo.AnyAsync();
            if (!anyRoom)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var cinemaId = Guid.Parse("55555555-5555-5555-5555-555555555555");

                await roomRepo.AddAsync(new Room
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Name = "Room 1",
                    CinemaId = cinemaId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await roomRepo.AddAsync(new Room
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Name = "Room 2",
                    CinemaId = cinemaId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial room data.");
            }
            else
            {
                logger.LogInformation("Room data already exists. Skipping seed.");
            }
        }

        private static async Task SeedSeats(IUnitOfWork unitOfWork, ILogger logger)
        {
            var seatRepo = unitOfWork.Repository<Seat>();
            var anySeat = await seatRepo.AnyAsync();
            if (!anySeat)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var roomId = Guid.Parse("77777777-7777-7777-7777-777777777777");

                await seatRepo.AddAsync(new Seat
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    SeatNumber = "A1",
                    RoomId = roomId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await seatRepo.AddAsync(new Seat
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    SeatNumber = "A2",
                    RoomId = roomId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await seatRepo.AddAsync(new Seat
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    SeatNumber = "B1",
                    RoomId = roomId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await seatRepo.AddAsync(new Seat
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    SeatNumber = "B2",
                    RoomId = roomId,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial seat data.");
            }
            else
            {
                logger.LogInformation("Seat data already exists. Skipping seed.");
            }
        }

        private static async Task SeedMovies(IUnitOfWork unitOfWork, ILogger logger)
        {
            var movieRepo = unitOfWork.Repository<Movie>();
            var anyMovie = await movieRepo.AnyAsync();
            if (!anyMovie)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");

                await movieRepo.AddAsync(new Movie
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Title = "Inception",
                    Description = "A thief who steals corporate secrets through dream infiltration technology.",
                    Genre = Genre.SciFi,
                    Duration = 148,
                    PosterPath = "/images/inception.jpg",
                    Status = MovieStatus.Active,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await movieRepo.AddAsync(new Movie
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Title = "The Dark Knight",
                    Description = "Batman faces the Joker, a criminal mastermind.",
                    Genre = Genre.Action,
                    Duration = 152,
                    PosterPath = "/images/darkknight.jpg",
                    Status = MovieStatus.Active,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial movie data.");
            }
            else
            {
                logger.LogInformation("Movie data already exists. Skipping seed.");
            }
        }

        private static async Task SeedShowtimes(IUnitOfWork unitOfWork, ILogger logger)
        {
            var showtimeRepo = unitOfWork.Repository<Showtime>();
            var anyShowtime = await showtimeRepo.AnyAsync();
            if (!anyShowtime)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var movieId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
                var roomId1 = Guid.Parse("77777777-7777-7777-7777-777777777777");
                var roomId2 = Guid.Parse("88888888-8888-8888-8888-888888888888");

                await showtimeRepo.AddAsync(new Showtime
                {
                    MovieId = movieId,
                    RoomId = roomId1,
                    StartTime = SeedDate.AddHours(2),
                    Price = 10.00m,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await showtimeRepo.AddAsync(new Showtime
                {
                    MovieId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    RoomId = roomId2,
                    StartTime = SeedDate.AddHours(4),
                    Price = 12.00m,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial showtime data.");
            }
            else
            {
                logger.LogInformation("Showtime data already exists. Skipping seed.");
            }
        }

        private static async Task SeedBookings(IUnitOfWork unitOfWork, ILogger logger)
        {
            var bookingRepo = unitOfWork.Repository<Booking>();
            var anyBooking = await bookingRepo.AnyAsync();
            if (!anyBooking)
            {
                var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");

                await bookingRepo.AddAsync(new Booking
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    UserId = userId,
                    ShowtimeId = 1,
                    BookingTime = SeedDate.DateTime,
                    TotalPrice = 20.00m,
                    Status = BookingStatus.Pending,
                    CreatedAt = SeedDate,
                    CreatedBy = adminId
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial booking data.");
            }
            else
            {
                logger.LogInformation("Booking data already exists. Skipping seed.");
            }
        }

        private static async Task SeedBookingDetails(IUnitOfWork unitOfWork, ILogger logger)
        {
            var bookingDetailRepo = unitOfWork.Repository<BookingDetail>();
            var anyBookingDetail = await bookingDetailRepo.AnyAsync();
            if (!anyBookingDetail)
            {
                var bookingId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
                var seatId1 = Guid.Parse("99999999-9999-9999-9999-999999999999");
                var seatId2 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

                await bookingDetailRepo.AddAsync(new BookingDetail
                {
                    BookingId = bookingId,
                    SeatId = seatId1,
                    Price = 10.00m
                });

                await bookingDetailRepo.AddAsync(new BookingDetail
                {
                    BookingId = bookingId,
                    SeatId = seatId2,
                    Price = 10.00m
                });

                await unitOfWork.CompleteAsync();
                logger.LogInformation("Seeded initial booking detail data.");
            }
            else
            {
                logger.LogInformation("Booking detail data already exists. Skipping seed.");
            }
        }
    }
}