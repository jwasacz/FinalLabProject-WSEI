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
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddRefuels_AddsRefuel_ReturnsUpdatedList()
        {
     
            var newRefuel = new Refuel { Date = DateTime.UtcNow, Price = 100 };

           
            var result = await _controller.AddRefuels(newRefuel);

      
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Refuel>>(okResult.Value);
            Assert.Single(refuels);
            Assert.Contains(refuels, r => r.Price == 100 && r.Date.Date == DateTime.UtcNow.Date);
        }
    }
}