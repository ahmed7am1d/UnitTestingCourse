using FakeItEasy;
using RunGroopWebApp.Interfaces;


namespace RunGroopWebApp.Tests.Controller;
public class ClubControllerTest
{
    private IClubRepository _clubRepository;
    private IPhotoService _photoService;
    public ClubControllerTest()
    {
        //DI
        _clubRepository = A.Fake<IClubRepository>();
        _photoService = A.Fake<IPhotoService>();
    }


    [Fact]
    public void Test1()
    {

    }
}
