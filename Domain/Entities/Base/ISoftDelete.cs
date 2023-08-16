namespace Domain.Entities.Base;

public interface ISoftDelete
{
    DateTime? DeletedOn { get; set; }
}