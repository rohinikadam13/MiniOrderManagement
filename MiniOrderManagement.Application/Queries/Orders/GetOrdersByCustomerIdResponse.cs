namespace MiniOrderManagement.Application.Queries.Orders
{
    public class GetOrdersByCustomerIdResponse
    {
        public List<OrderDto> Orders { get; set; } = new();
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}