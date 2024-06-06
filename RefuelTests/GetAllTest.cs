using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;
using Xunit;

namespace RefuelTests
{
    public class RefuelGetAllTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RefuelController _controller;

        public RefuelGetAllTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);

            // Dodajemy kilka testowych rekordów
            _context.Refuels.AddRange(
                new Refuel { Date = DateTime.UtcNow.AddDays(-1), Price = 100 },
                new Refuel { Date = DateTime.UtcNow, Price = 150 }
            );
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetAllRefuels_ReturnsAllRefuels()
        {
            // Act
            var result = await _controller.GetAllRefuels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Refuel>>(okResult.Value);
            var expectedCount = await _context.Refuels.CountAsync();
            Assert.Equal(expectedCount, refuels.Count);
        }
    }
}