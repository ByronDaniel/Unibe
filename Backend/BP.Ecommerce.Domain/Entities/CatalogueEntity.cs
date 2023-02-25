using System.ComponentModel.DataAnnotations;

namespace BP.Ecommerce.Domain.Entities
{
    public class CatalogueEntity : AuditoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
