namespace MiniOrderManagement.Application.DTOs.Responses
{
    /// <summary>
    /// Response after creating an order
    /// </summary>
    public class CreateOrderResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Message { get; set; } = "Order created successfully";
    }
}