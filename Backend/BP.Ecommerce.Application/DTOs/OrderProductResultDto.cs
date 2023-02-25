namespace BP.Ecommerce.Domain.Entities
{
    public class OrderProductResultDto
    {
        public Guid ProductId { get; set; }
        public string Product { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock{ get; set; }
        public int ProductQuantity { get; set; }
        public decimal Total { get; set; }
    }
}
