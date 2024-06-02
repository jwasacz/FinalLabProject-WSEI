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
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbFinalLabWSEI;Trusted_Connection=True;MultipleActiveResultSets=true")  
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
        public async Task GetAllRefuels_ShouldReturnAllRefuels()
        {
            // Arrange
            var newRefuel1 = new Refuel { Date = DateTime.Now, Price = 20 };
            var newRefuel2 = new Refuel { Date = DateTime.Now.AddDays(-1), Price = 30 };
            await _context.Refuels.AddAsync(newRefuel1);
            await _context.Refuels.AddAsync(newRefuel2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllRefuels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var refuels = Assert.IsType<List<Refuel>>(okResult.Value);
            Assert.Equal(2, refuels.Count);
            Assert.Contains(refuels, r => r.Price == 20);
            Assert.Contains(refuels, r => r.Price == 30);
        }
    }
}
