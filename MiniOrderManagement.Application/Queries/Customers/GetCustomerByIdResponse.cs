using MiniOrderManagement.Domain.Models;

namespace MiniOrderManagement.Application.Queries.Customers
{
    /// <summary>
    /// Response DTO for GetCustomerById query
    /// Includes customer details, profile, and orders
    /// </summary>
    public class GetCustomerByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CustomerProfileDto Profile { get; set; } = new();
        public List<OrderDto> Orders { get; set; } = new();
    }

    /// <summary>
    /// Order DTO for response
    /// </summary>
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}