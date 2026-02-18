using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Domain.Models
{
    public class Customer
    {
        private readonly List<Order> _orders = new();

        public int Id { get; private set; }
        public string Name { get; private set; }

        public CustomerProfile Profile { get; private set; }

        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private Customer() { }

        public Customer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name is required");

            Name = name;
        }

        public void AddProfile(CustomerProfile profile)
        {
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orders.Add(order);
        }
    }
}
