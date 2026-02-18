using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Queries.Orders;

namespace MiniOrderManagement.Application.Queries.Orders
{
    public class GetOrdersByCustomerIdHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetOrdersByCustomerIdResponse> Handle(GetOrdersByCustomerIdQuery query)
        {
            var orders = await _unitOfWork.Orders.GetByCustomerIdAsync(query.CustomerId);

            return new GetOrdersByCustomerIdResponse
            {
                Orders = orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount
                }).ToList()
            };
        }
    }
}