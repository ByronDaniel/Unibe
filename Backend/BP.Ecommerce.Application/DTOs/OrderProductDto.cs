namespace BP.Ecommerce.Domain.Entities
{
    public class OrderProductDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Total { get; set; }
    }
}
