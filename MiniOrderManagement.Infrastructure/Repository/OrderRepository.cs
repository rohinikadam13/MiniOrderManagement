using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Application.Interfaces.Repositories;
using MiniOrderManagement.Domain.Models;
using MiniOrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
            => await _context.Orders.AddAsync(order);

        public async Task<List<Order>> GetByCustomerIdAsync(int customerId)
            => await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
    }

}
