namespace MiniOrderManagement.Application.DTOs.Commands
{
    /// <summary>
    /// DTO for creating a new customer with profile
    /// </summary>
    public record CreateCustomerDto(
        string Name,
        string Address,
        string Phone
    );
}