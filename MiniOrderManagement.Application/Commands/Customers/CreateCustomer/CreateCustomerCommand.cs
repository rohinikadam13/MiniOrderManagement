using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Commands.Customers.CreateCustomer
{
    public record CreateCustomerCommand(string Name, string Address, string Phone);
}
