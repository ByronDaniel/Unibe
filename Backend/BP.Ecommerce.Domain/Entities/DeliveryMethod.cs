using System.ComponentModel.DataAnnotations;

namespace BP.Ecommerce.Domain.Entities
{
    public class DeliveryMethod : CatalogueEntity
    {
        public decimal PriceByKm { get; set; } = 0;
    }
}
