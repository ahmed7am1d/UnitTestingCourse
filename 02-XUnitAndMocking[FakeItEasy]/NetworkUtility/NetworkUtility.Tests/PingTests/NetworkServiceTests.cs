using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;


namespace NetworkUtility.Tests.PingTests;
public class NetworkServiceTests
{
    private readonly NetworkService _networkService;
    private readonly IDNS _dns;
    public NetworkServiceTests()
    {
        //Dependencies - Mocks
        _dns = A.Fake<IDNS>();
        //SUT
        _networkService = new NetworkService(_dns);
    }

    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        //Arrange - variables, classes, mocks
        A.CallTo(() => _dns.SendDNSRequest()).Returns(true);

        //Act
        var pingResult = _networkService.SendPing();

        //Assert
        pingResult.Should().NotBeNullOrWhiteSpace();
        pingResult.Should().Be("Success: Ping Sent!");
        pingResult.Should().Contain("Success", Exactly.Once());

    }
    //Theory used => when we can put inline data
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 1, 3)]
    public void NetworkService_PingTimeOut_ReturnInteger(int a, int b, int expected)
    {
        //Arrange - variables, classes, mocks


        //Act
        var pingResult = _networkService.PingTimeOut(a, b);

        //Assert
        pingResult.Should().Be(expected);
        pingResult.Should().BeGreaterThanOrEqualTo(expected);
        pingResult.Should().NotBeInRange(-1000, 0);
    }

    [Fact]
    public void NetworkService_LastPingDate_ReturnDate()
    {
        //Arrange - variables, classes, mocks 

        //Act 
        var result = _networkService.LastPingDate();

        //Assert
        result.Should().BeAfter(1.January(2012));
        result.Should().BeBefore(1.January(2027));
    }

    //Most of the time when testing objects we need first thing to make sure of the type

    [Fact]
    public void NetworkService_GetPingOptions_ReturnObject()
    {
        //Arrange - varaibles, classes, mocks 
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };

        //Act
        var result = _networkService.GetPingOptions();

        //Assert WARNING: "Be" careful
        result.Should().BeOfType<PingOptions>();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected);
        result.Ttl.Should().Be(1);
    }

    [Fact]
    public void NetworkService_GetMostRecentPings_ReturnArray()
    {
        //Arrange - varaibles, classes, mocks 
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };


        //Act 
        var result = _networkService.GetMostRecentPings();


        //Assert
        result.Should().BeOfType<PingOptions[]>();
        result.Should().NotBeNull();
        result.Should().ContainEquivalentOf(expected);
        result.Should().Contain(x => x.DontFragment == true);

    }
}
