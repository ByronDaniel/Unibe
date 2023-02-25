namespace BP.Ecommerce.Domain.Entities
{
    public class AuditoryEntity : BaseEntity
    {
        public DateTime DateDeleted { get; set; }
        public string State { get; set; } = Status.Vigente.ToString();
    }
}
