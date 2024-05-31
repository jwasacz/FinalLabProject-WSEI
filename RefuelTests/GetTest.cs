using Xunit;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Entities;
using WebAPI.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace RefuelTests
{
    public class RefuelTests
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("DbFinalLabWSEI") 
                .Options;

            var context = new DataContext(options);
            // Seed the database with initial data if necessary
            context.Refuels.Add(new Refuel { Date = System.DateTime.Now, Price = 10 /* initial properties */ });
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task AddRefuels_ShouldAddRefuelAndReturnList()
        {
            // Arrange
            var context = await GetDbContext();
            var controller = new RefuelController(context);
            var newRefuel = new Refuel
            {
                Date = System.DateTime.Now,
                Price = 200 // Ustaw właściwości dla nowego tankowania
            };

            // Act
            var actionResult = await controller.AddRefuels(newRefuel);

            // Assert
            Assert.NotNull(actionResult);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var value = okResult.Value as List<Refuel>;
            Assert.NotNull(value);
            Assert.Contains(value, r => r.Price == newRefuel.Price); // Dostosuj sprawdzanie właściwości w razie potrzeby
        }

    }
}
