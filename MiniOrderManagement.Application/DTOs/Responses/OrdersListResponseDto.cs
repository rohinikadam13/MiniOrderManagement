namespace MiniOrderManagement.Application.DTOs.Responses
{
    /// <summary>
    /// Wrapper for multiple orders response
    /// </summary>
    public class OrdersListResponseDto
    {
        public List<OrderResponseDto> Orders { get; set; } = new();
        public int TotalCount { get; set; }
    }
}