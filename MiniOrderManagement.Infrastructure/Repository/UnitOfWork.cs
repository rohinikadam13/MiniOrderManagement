using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Interfaces.Repositories;
using MiniOrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(context);
            Orders = new OrderRepository(context);
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }

}
