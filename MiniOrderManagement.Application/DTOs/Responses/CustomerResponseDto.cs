namespace MiniOrderManagement.Application.DTOs.Responses
{
    /// <summary>
    /// Complete customer response with profile and orders
    /// </summary>
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CustomerProfileResponseDto Profile { get; set; } = new();
        public List<OrderResponseDto> Orders { get; set; } = new();
    }

    /// <summary>
    /// Customer profile response
    /// </summary>
    public class CustomerProfileResponseDto
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}