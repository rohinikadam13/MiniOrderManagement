using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Exceptions;
using MiniOrderManagement.Domain.Models;

namespace MiniOrderManagement.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateOrderCommand command)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(command.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException(command.CustomerId);

            var order = new Order(command.OrderDate, command.TotalAmount);
            customer.AddOrder(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
