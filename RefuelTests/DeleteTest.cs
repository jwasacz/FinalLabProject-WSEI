using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Entities;

namespace RefuelTests
{
    public class RefuelDeleteTests
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("DbFinalLabWSEI")
                .Options;
          //  DataContext DbContext = new DataContext();

            var context = new DataContext(options);
            // Seed the database with initial data if necessary
            context.Refuels.Add(new WebAPI.Entities.Refuel {Date = System.DateTime.Now,Price=10 /* initial properties */ });
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
                // Set properties for the new refuel
            };

            // Act
           // var result = await controller.AddRefuels(newRefuel) as OkObjectResult;
          //  var value = result.Value as List<Refuel>;

            // Assert
          //  Assert.NotNull(result);
         //   Assert.Equal(200, result.StatusCode);
          //  Assert.NotNull(value);
          //  Assert.Contains(value, r => r.Property == newRefuel.Property); // Adjust the property check as necessary
        }

        //weryfikacja autoryzacji metody - oczekiwanie na 401
        //Assert.Equal(401, result.StatusCode);
    }
}