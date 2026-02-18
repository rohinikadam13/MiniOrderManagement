using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Application.Commands.Customers.CreateCustomer
{
    public class CreateCustomerHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Name);
            var profile = new CustomerProfile(command.Address, command.Phone);

            customer.AddProfile(profile);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer.Id;
        }
    }

}
