namespace MiniOrderManagement.Application.DTOs.Responses
{
    /// <summary>
    /// Order response DTO
    /// </summary>
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
}