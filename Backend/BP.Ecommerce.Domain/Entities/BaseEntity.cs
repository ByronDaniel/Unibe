namespace BP.Ecommerce.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime DateModification { get; set; }
    }
}
