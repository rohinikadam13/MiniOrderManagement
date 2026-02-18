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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
            => await _context.Customers.AddAsync(customer);

        public async Task<Customer?> GetByIdAsync(int id)
            => await _context.Customers
                .Include(c => c.Profile)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
    }

}
