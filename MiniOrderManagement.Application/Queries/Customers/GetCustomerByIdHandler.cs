using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Queries.Customers;

namespace MiniOrderManagement.Application.Queries.Customers
{
    /// <summary>
    /// Handler for GetCustomerById query
    /// Retrieves customer with profile and orders
    /// </summary>
    public class GetCustomerByIdHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCustomerByIdResponse?> Handle(GetCustomerByIdQuery query)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(query.Id);

            if (customer == null)
                return null;

            return new GetCustomerByIdResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Profile = new CustomerProfileDto
                {
                    Address = customer.Profile?.Address ?? string.Empty,
                    Phone = customer.Profile?.PhoneNumber ?? string.Empty
                },
                Orders = customer.Orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount
                }).ToList()
            };
        }
    }
}