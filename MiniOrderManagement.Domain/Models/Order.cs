using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOrderManagement.Domain.Models
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }

        public int CustomerId { get; private set; }

        private Order() { }

        public Order(DateTime orderDate, decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new ArgumentException("Total amount must be greater than zero");

            OrderDate = orderDate;
            TotalAmount = totalAmount;
        }
    }
}
