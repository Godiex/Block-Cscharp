namespace Domain.Entities.Base
{
    public class DomainEntity : ISoftDelete
    {
        public DateTime? DeletedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

        public void SetDelete() => DeletedOn = DateTime.Now;
    }
}
