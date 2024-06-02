using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;
using Xunit;

namespace RefuelTests
{
    public class RefuelGetByIdTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RefuelController _controller;

        public RefuelGetByIdTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbFinalLabWSEI;Trusted_Connection=True;MultipleActiveResultSets=true") 
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Usuwa bazę danych po zakończeniu testów
        }

        [Fact]
        public async Task GetAllRefuels_ValidId_ReturnsRefuel()
        {
            // Arrange
            var newRefuel = new Refuel { Date = DateTime.Now, Price = 20 };
            await _context.Refuels.AddAsync(newRefuel);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllRefuels(newRefuel.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuelResult = Assert.IsType<Refuel>(okResult.Value);
            Assert.Equal(newRefuel.Id, refuelResult.Id);
            Assert.Equal(newRefuel.Price, refuelResult.Price);
        }

        [Fact]
        public async Task GetAllRefuels_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            int invalidId = 999; // Zakładając, że ID 999 nie istnieje

            // Act
            var result = await _controller.GetAllRefuels(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Refuel not found.", badRequestResult.Value);
        }
    }
}
