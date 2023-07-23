using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Tests.Controller;
public class ClubControllerTest
{
    private IClubRepository _clubRepository;
    private IPhotoService _photoService;
    private ClubController _clubController;
    public ClubControllerTest()
    {
        //DI
        _clubRepository = A.Fake<IClubRepository>();
        _photoService = A.Fake<IPhotoService>();

        //SUT
        //SUT => is actually what we are executing on 
        //We litterally want to test them as alone not the actual code :) 
        _clubController = new ClubController(_clubRepository,_photoService);

    }


    [Fact]
    public void ClubController_Index_ReturnSuccess()
    {
        //Arrange - What do I need to bring in 
        //I know that this method inside the ClubController returns IEnumberable list of club so I need to fake it
        /*
         1- Creating a fake list of Clubs using FakeItEasy 
         2- Make call to that repositroy method expecting that it will returns the list of clubs 
         */

        var clubs = A.Fake<IEnumerable<Club>>();
        A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);


        //Act 
        var result = _clubController.Index();

        //Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClubController_DetailClub_ReturnSuccess()
    {

        //Arrange 
        var id = 1;
        var club = A.Fake<Club>();
        A.CallTo(()=>_clubRepository.GetByIdAsync(id)).Returns(club);


        //Act
        var result = _clubController.DetailClub(id,"");

        //Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }
}
