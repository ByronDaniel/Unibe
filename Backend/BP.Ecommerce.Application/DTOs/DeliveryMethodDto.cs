namespace BP.Ecommerce.Application.DTOs
{
    public class DeliveryMethodDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal PriceByKm { get; set; }
    }
}
