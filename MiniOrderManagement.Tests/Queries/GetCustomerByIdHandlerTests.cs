using FluentAssertions;
using Moq;
using MiniOrderManagement.Application.Queries.Customers;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Interfaces.Repositories;
using MiniOrderManagement.Domain.Models;
using Xunit;

namespace MiniOrderManagement.Tests.Queries
{
    public class GetCustomerByIdHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidId_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer("John Doe");
            var profile = new CustomerProfile("123 Main St", "5551234567");
            customer.AddProfile(profile);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCustomerRepo = new Mock<ICustomerRepository>();

            mockUnitOfWork.Setup(x => x.Customers).Returns(mockCustomerRepo.Object);
            mockCustomerRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(customer);

            var handler = new GetCustomerByIdHandler(mockUnitOfWork.Object);
            var query = new GetCustomerByIdQuery(1);

            // Act
            var result = await handler.Handle(query);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("John Doe");
            result.Profile.Address.Should().Be("123 Main St");
            result.Profile.Phone.Should().Be("5551234567");
        }

        [Fact]
        public async Task Handle_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Customers.GetByIdAsync(999)).ReturnsAsync((Customer)null);

            var handler = new GetCustomerByIdHandler(mockUnitOfWork.Object);
            var query = new GetCustomerByIdQuery(999);

            // Act
            var result = await handler.Handle(query);

            // Assert
            result.Should().BeNull();
        }
    }
}