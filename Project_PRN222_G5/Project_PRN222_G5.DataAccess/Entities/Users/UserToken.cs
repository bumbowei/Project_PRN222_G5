using Project_PRN222_G5.DataAccess.Entities.Common;

namespace Project_PRN222_G5.DataAccess.Entities.Users;

public class UserToken : BaseEntity
{
    public string RefreshToken { get; set; } = string.Empty;

    public string? ClientIp { get; set; } = string.Empty;

    public DateTimeOffset ExpiredTime { get; set; } = default;

    public Guid UserId { get; set; } = Guid.Empty;

    public User? User { get; set; } = default;
}