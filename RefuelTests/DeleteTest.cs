using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;
using Xunit;

namespace RefuelTests
{
    public class RefuelDeleteTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RefuelController _controller;

        public RefuelDeleteTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);

            
            _context.Refuels.Add(new Refuel { Date = DateTime.UtcNow, Price = 100 });
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task DeleteRefuels_DeletesRefuel_ReturnsUpdatedList()
        {
            
            var existingRefuel = await _context.Refuels.FirstAsync();
            var initialCount = await _context.Refuels.CountAsync();

           
            var result = await _controller.DeleteRefuels(existingRefuel.Id);

           
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Refuel>>(okResult.Value);
            Assert.Equal(initialCount - 1, refuels.Count);
            Assert.DoesNotContain(refuels, r => r.Id == existingRefuel.Id);
        }
    }
}