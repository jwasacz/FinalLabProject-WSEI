using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;
using Xunit;

namespace RefuelTests
{
    public class RefuelAddTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RefuelController _controller;

        public RefuelAddTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbFinalLabWSEI;Trusted_Connection=True;MultipleActiveResultSets=true") // Zmień to na właściwy connection string
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
        public async Task AddRefuels_AddsRefuel_ReturnsUpdatedList()
        {
            // Arrange
            var newRefuel = new Refuel { Date = DateTime.Now, Price = 100 };

            // Act
            var result = await _controller.AddRefuels(newRefuel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Refuel>>(okResult.Value);
            Assert.Single(refuels); // Sprawdza, czy lista ma dokładnie jeden element
            Assert.Contains(refuels, r => r.Price == 100 && r.Date.Date == DateTime.Now.Date);
        }
    }
}
