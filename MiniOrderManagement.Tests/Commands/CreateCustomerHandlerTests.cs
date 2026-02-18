using FluentAssertions;
using Moq;
using MiniOrderManagement.Application.Commands.Customers.CreateCustomer;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Models;
using Xunit;

namespace MiniOrderManagement.Tests.Commands
{
    public class CreateCustomerHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidCommand_ShouldCreateCustomer()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateCustomerHandler(mockUnitOfWork.Object);
            var command = new CreateCustomerCommand("John Doe", "123 Main St", "5551234567");

            // Act
            var result = await handler.Handle(command);

            // Assert
            result.Should().BeGreaterThan(0);
            mockUnitOfWork.Verify(x => x.Customers.AddAsync(It.IsAny<Customer>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task Handle_WithInvalidName_ShouldThrowException(string invalidName)
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var handler = new CreateCustomerHandler(mockUnitOfWork.Object);
            var command = new CreateCustomerCommand(invalidName, "123 Main St", "5551234567");

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command));
        }
    }
}