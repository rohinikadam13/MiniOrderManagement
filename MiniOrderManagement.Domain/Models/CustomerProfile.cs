using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Domain.Models
{
    public class CustomerProfile
    {
        public int Id { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public int CustomerId { get; private set; }

        private CustomerProfile() { }

        public CustomerProfile(string address, string phone)
        {
            Address = address;
            PhoneNumber = phone;
        }
    }
}
