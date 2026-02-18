using FluentAssertions;
using Moq;
using MiniOrderManagement.Application.Commands.Orders.CreateOrder;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Interfaces.Repositories;
using MiniOrderManagement.Domain.Exceptions;
using MiniOrderManagement.Domain.Models;
using Xunit;

namespace MiniOrderManagement.Tests.Commands
{
    public class CreateOrderHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateOrder()
        {
            // Arrange
            var customer = new Customer("Test Customer");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();

            mockUnitOfWork.Setup(x => x.Customers).Returns(mockCustomerRepo.Object);
            mockCustomerRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);

            var handler = new CreateOrderHandler(mockUnitOfWork.Object);
            var command = new CreateOrderCommand(1, DateTime.UtcNow.AddDays(-1), 100.50m);

            // Act
            await handler.Handle(command);

            // Assert
            customer.Orders.Should().HaveCount(1);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistentCustomer_ShouldThrowCustomerNotFoundException()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Customers.GetByIdAsync(999)).ReturnsAsync((Customer)null);

            var handler = new CreateOrderHandler(mockUnitOfWork.Object);
            var command = new CreateOrderCommand(999, DateTime.UtcNow.AddDays(-1), 100.50m);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => handler.Handle(command));
        }

        [Fact]
        public async Task Handle_WithZeroAmount_ShouldThrowException()
        {
            // Arrange
            var customer = new Customer("Test Customer");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Customers.GetByIdAsync(1)).ReturnsAsync(customer);

            var handler = new CreateOrderHandler(mockUnitOfWork.Object);
            var command = new CreateOrderCommand(1, DateTime.UtcNow.AddDays(-1), 0);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command));
        }
    }
}