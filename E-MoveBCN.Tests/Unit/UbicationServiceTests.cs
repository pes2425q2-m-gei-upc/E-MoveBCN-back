using Xunit;
using Moq;
using FluentAssertions;
using Services;
using Services.Interface;
using Repositories.Interface;
using src.Helpers.Interface;
using Dto;
using TestUtils;
using plantilla.Web.src.Services.Interface;

namespace Unit;

public class UbicationServiceTests
{
    private readonly Mock<IUbicationRepository> _ubicationRepoMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IBicingStationRepository> _bicingRepoMock = new();
    private readonly Mock<IChargingStationsRepository> _chargingRepoMock = new();
    private readonly Mock<IAireLliureHelper> _aireLliureHelperMock = new();
    private readonly Mock<IStateBicingRepository> _stateBicingRepoMock = new();
    private readonly Mock<ITmbService> _tmbServiceMock = new();

    private readonly IUbicationService _ubicationService;

    public UbicationServiceTests()
    {
        _ubicationService = new UbicationService(
            _userRepoMock.Object,
            _ubicationRepoMock.Object,
            _bicingRepoMock.Object,
            _chargingRepoMock.Object,
            _aireLliureHelperMock.Object,
            _stateBicingRepoMock.Object,
            _tmbServiceMock.Object
        );
    }

    [Fact]
    public async Task GetUbicationsByUserIdAsync_ShouldReturnList()
    {
        var ubications = new List<SavedUbicationDto> { TestUbicationFactory.CreateSavedUbicationDto() };
        _ubicationRepoMock.Setup(r => r.GetUbicationsByEmailAsync("user@example.com")).ReturnsAsync(ubications);

        var result = await _ubicationService.GetUbicationsByUserIdAsync("user@example.com");

        result.Should().BeEquivalentTo(ubications);
    }

    [Fact]
    public async Task SaveUbicationAsync_ShouldReturnFalse_WhenUserNotFound()
    {
        var dto = TestUbicationFactory.CreateSavedUbicationDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto)null);

        var result = await _ubicationService.SaveUbicationAsync(dto);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task SaveUbicationAsync_ShouldReturnTrue_WhenUserExists()
    {
        var dto = TestUbicationFactory.CreateSavedUbicationDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
        _ubicationRepoMock.Setup(r => r.SaveUbicationAsync(dto)).ReturnsAsync(true);

        var result = await _ubicationService.SaveUbicationAsync(dto);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteUbication_ShouldReturnFalse_WhenUserNotFound()
    {
        var dto = TestUbicationFactory.CreateUbicationInfoDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto)null);

        var result = await _ubicationService.DeleteUbication(dto);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteUbication_ShouldReturnTrue_WhenUserExists()
    {
        var dto = TestUbicationFactory.CreateUbicationInfoDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
        _ubicationRepoMock.Setup(r => r.DeleteUbication(dto)).ReturnsAsync(true);

        var result = await _ubicationService.DeleteUbication(dto);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateUbication_ShouldReturnFalse_WhenUserNotFound()
    {
        var dto = TestUbicationFactory.CreateUbicationInfoDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto)null);

        var result = await _ubicationService.UpdateUbication(dto);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateUbication_ShouldReturnTrue_WhenUserExists()
    {
        var dto = TestUbicationFactory.CreateUbicationInfoDto();
        _userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
        _ubicationRepoMock.Setup(r => r.UpdateUbication(dto)).ReturnsAsync(true);

        var result = await _ubicationService.UpdateUbication(dto);

        result.Should().BeTrue();
    }
}
