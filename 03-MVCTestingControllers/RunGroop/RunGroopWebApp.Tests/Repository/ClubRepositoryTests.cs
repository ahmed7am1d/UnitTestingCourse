using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Repository;
public class ClubRepositoryTests
{
    //private IClubRepository _clubRepository;
    public ClubRepositoryTests()
    {
        //DI
        //_clubRepository = A.Fake<IClubRepository>();
    }

    private async Task<ApplicationDbContext> GetDbContext()
    {
        //Create database options
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        //Create the dbContext
        var dataBaseContext = new ApplicationDbContext(options);
        //If database is not created => create it
        dataBaseContext.Database.EnsureCreated();
        //Fill the database if empty
        if (await dataBaseContext.Clubs.CountAsync() <= 0)
        {
            for (byte i = 0; i < 10; i++)
            {
                dataBaseContext.Clubs.Add(
     new Club()
     {
         Title = "Running Club 1",
         Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
         Description = "This is the description of the first cinema",
         ClubCategory = ClubCategory.City,
         Address = new Address()
         {
             Street = "123 Main St",
             City = "Charlotte",
             State = "NC"
         }
     });

                await dataBaseContext.SaveChangesAsync();
            }

        }
        return dataBaseContext;
    }

    [Fact]
    public async void ClubRepository_Add_ReturnsBool()
    {
        //Arrange 
        var club = new Club()
        {
            Title = "Running Club 1",
            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
            Description = "This is the description of the first cinema",
            ClubCategory = ClubCategory.City,
            Address = new Address()
            {
                Street = "123 Main St",
                City = "Charlotte",
                State = "NC"
            }
        };


        //Act 
        var dbContext = await GetDbContext();
        var clubRepository = new ClubRepository(dbContext);
        var result = clubRepository.Add(club);

        //Assert
        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(1)]
    public async void ClubRepository_GetByIdAsync_ReturnsClub(int clubId)
    {
        //Arrange 
        var dbContext = await GetDbContext();
        var clubRepository = new ClubRepository(dbContext);

        //Act 
        var result = await clubRepository.GetByIdAsync(clubId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Club>();
    }

}
