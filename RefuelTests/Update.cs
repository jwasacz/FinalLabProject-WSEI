using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;
using Xunit;

namespace RefuelTests
{
    public class RefuelUpdateTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RefuelController _controller;

        public RefuelUpdateTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbFinalLabWSEI;Trusted_Connection=True;MultipleActiveResultSets=true")  // Zmień to na właściwy connection string
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();  // Usuwa bazę danych po zakończeniu testów
        }

        [Fact]
        public async Task UpdateRefuels_ValidId_UpdatesAndReturnsRefuel()
        {
            // Arrange
            var existingRefuel = new Refuel { Date = DateTime.Now.AddDays(-1), Price = 100 };
            await _context.Refuels.AddAsync(existingRefuel);
            await _context.SaveChangesAsync();

            var updatedRefuel = new Refuel { Id = existingRefuel.Id, Date = DateTime.Now, Price = 150 };

            // Act
            var result = await _controller.UpdateRefuels(updatedRefuel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultRefuel = Assert.IsType<Refuel>(okResult.Value);
            Assert.Equal(150, resultRefuel.Price);
            Assert.Equal(updatedRefuel.Date.Date, resultRefuel.Date.Date);
        }

        [Fact]
        public async Task UpdateRefuels_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var updatedRefuel = new Refuel { Id = 999, Date = DateTime.Now, Price = 150 };  // ID 999 assumed not to exist

            // Act
            var result = await _controller.UpdateRefuels(updatedRefuel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Refuel not found.", badRequestResult.Value);
        }
    }
}
