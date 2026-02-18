using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Queries.Orders
{
    public record GetOrdersByCustomerIdQuery(int CustomerId);
}
