using FluentAssertions;
using LoginMVC_Database.Data;
using LoginMVC_Database.Models;
using LoginMVC_Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoginMVC_Database.Tests.Repository
{
    public class LoginMVCTests
    {
        private async Task<LoginMVC_DbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LoginMVC_DbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).
                Options;

            var databaseContext = new LoginMVC_DbContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

        [Fact]
        public async void LoginMVCTests_Add_ReturnsBool()
        {
            //Arrange
            var fakePerson = new FakePersonData()
            {
                FirstName = "John",
                LastName = "Jonnathan",
                Age = 25


            };
            var dbContext = await GetDbContext();
            var loginMVCRepository = new LoginMVCRepository(dbContext);

            //Act
            var result = loginMVCRepository.AddFakePerson(fakePerson);

            //Assert
            result.Should().BeTrue();

        }


    }
}