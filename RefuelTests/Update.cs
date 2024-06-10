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
                .UseInMemoryDatabase(databaseName: "TestDbForUpdate")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);

        
            var refuel = new Refuel { Id = 1, Date = DateTime.UtcNow.AddDays(-1), Price = 100 };
            _context.Refuels.Add(refuel);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateRefuel_ReturnsUpdatedRefuel()
        {
           
            var updatedRefuel = new Refuel { Id = 1, Date = DateTime.UtcNow, Price = 150 };

          
            var result = await _controller.UpdateRefuels(updatedRefuel);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuel = Assert.IsType<Refuel>(okResult.Value);
            Assert.Equal(updatedRefuel.Date, refuel.Date);
            Assert.Equal(updatedRefuel.Price, refuel.Price);
        }

        [Fact]
        public async Task UpdateRefuel_ReturnsBadRequest_WhenRefuelNotFound()
        {
            
            var updatedRefuel = new Refuel { Id = 99, Date = DateTime.UtcNow, Price = 150 };

            
            var result = await _controller.UpdateRefuels(updatedRefuel);

            
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}