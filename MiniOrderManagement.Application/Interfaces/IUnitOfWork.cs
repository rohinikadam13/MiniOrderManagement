using MiniOrderManagement.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }

        Task<int> SaveChangesAsync();
    }

}
