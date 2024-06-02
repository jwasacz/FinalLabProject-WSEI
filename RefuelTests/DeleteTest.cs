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
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbFinalLabWSEI;Trusted_Connection=True;MultipleActiveResultSets=true")  // Zmieñ to na w³aœciwy connection string
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureCreated();
            _controller = new RefuelController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();  // Usuwa bazê danych po zakoñczeniu testów
        }

        [Fact]
        public async Task DeleteRefuels_ShouldRemoveRefuelAndReturnUpdatedList()
        {
            // Arrange
            var newRefuel = new Refuel { Date = DateTime.Now, Price = 20 };
            var addResult = await _controller.AddRefuels(newRefuel);
            Assert.NotNull(addResult);

            var okAddResult = addResult.Result as OkObjectResult;
            Assert.NotNull(okAddResult);
            var addedRefuel = ((List<Refuel>)okAddResult.Value).Last();

            // Act
            var deleteResult = await _controller.DeleteRefuels(addedRefuel.Id);
            Assert.NotNull(deleteResult);

            var okDeleteResult = deleteResult.Result as OkObjectResult;
            Assert.NotNull(okDeleteResult);

            // Assert
            var refuels = Assert.IsType<List<Refuel>>(okDeleteResult.Value);
            Assert.DoesNotContain(refuels, r => r.Id == addedRefuel.Id);
        }

        [Fact]
        public async Task DeleteRefuels_ShouldReturnBadRequestWhenRefuelDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteRefuels(999);  // Zak³adaj¹c, ¿e ID 999 nie istnieje

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Refuel not found.", badRequestResult.Value);
        }

    }
}
