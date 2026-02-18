using MiniOrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetByCustomerIdAsync(int customerId);
    }
}
