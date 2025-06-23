namespace Project_PRN222_G5.DataAccess.Entities.Common;

public abstract class BaseEntity : DefaultEntity, IBaseAuditable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid CreatedBy { get; set; } = default;
    public Guid? UpdatedBy { get; set; } = default;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public abstract class BaseEntity<T> : DefaultEntity<T>, IBaseAuditable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid CreatedBy { get; set; } = default;
    public Guid? UpdatedBy { get; set; } = default;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}