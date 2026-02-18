namespace MiniOrderManagement.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public int CustomerId { get; }

        public CustomerNotFoundException(int customerId)
            : base($"Customer with ID {customerId} does not exist")
        {
            CustomerId = customerId;
        }
    }
}