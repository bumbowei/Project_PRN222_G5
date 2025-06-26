using Project_PRN222_G5.DataAccess.Entities.Common;

namespace Project_PRN222_G5.DataAccess.Entities.Users;

public class UserResetPassword : BaseEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTimeOffset Expiry { get; set; } = default;

    public Guid UserId { get; set; } = Guid.Empty;

    public User? User { get; set; } = default;
}