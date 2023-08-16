using Domain.Entities.Base;

namespace Domain.Entities.Base
{
    public class EntityBase<T> : DomainEntity, IEntityBase<T>
    {
        public virtual T Id { get; set; } = default!;
    }
}


