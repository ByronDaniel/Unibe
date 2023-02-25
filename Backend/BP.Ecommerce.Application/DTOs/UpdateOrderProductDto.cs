namespace BP.Ecommerce.Application.DTOs
{
    public class UpdateOrderProductDto
    {
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
