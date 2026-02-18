using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Commands.Orders.CreateOrder
{
    public record CreateOrderCommand(int CustomerId, DateTime OrderDate, decimal TotalAmount);
}
