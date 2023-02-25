namespace BP.Ecommerce.Domain.Entities
{
    public class OrderGetAllDto
    {
        public Guid Id { get; set; }
        public virtual List<OrderProductResultDto>? orderProducts { get; set; }
        public string? DeliveryMethod { get; set; }
        public decimal? DeliveryMethodPrice { get; set; } = 0;
        public decimal Subtotal { get; set; } = 0;
        public decimal Iva { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public string State { get; set; }
    }
}
