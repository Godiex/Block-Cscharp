namespace Domain.Entities.Base;

public interface IEntityBase
{
}

public interface IEntityBase<T> : IEntityBase
{
    T Id { get; }
}
