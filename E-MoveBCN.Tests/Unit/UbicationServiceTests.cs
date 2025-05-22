using Xunit;
using Moq;
using FluentAssertions;
using Services;
using Services.Interface;
using Repositories.Interface;
using Helpers.Interface;
using TestUtils;
using Dto.Ubication;
using Dto.User;

namespace E_MoveBCN.Tests.Unit;

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
    this._ubicationService = new UbicationService(
        this._userRepoMock.Object,
        this._ubicationRepoMock.Object,
        this._bicingRepoMock.Object,
        this._chargingRepoMock.Object,
        this._aireLliureHelperMock.Object,
        this._stateBicingRepoMock.Object,
        this._tmbServiceMock.Object
    );
  }

  [Fact]
  public async Task GetUbicationsByUserIdAsync_ShouldReturnList()
  {
    var ubications = new List<SavedUbicationDto> { TestUbicationFactory.CreateSavedUbicationDto() };
    this._ubicationRepoMock.Setup(r => r.GetUbicationsByEmailAsync("user@example.com")).ReturnsAsync(ubications);

    var result = await this._ubicationService.GetUbicationsByUserIdAsync("user@example.com").ConfigureAwait(false);

    result.Should().BeEquivalentTo(ubications);
  }

  [Fact]
  public async Task SaveUbicationAsync_ShouldReturnFalse_WhenUserNotFound()
  {
    var dto = TestUbicationFactory.CreateSavedUbicationDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto?)null);

    var result = await this._ubicationService.SaveUbicationAsync(dto).ConfigureAwait(false);

    result.Should().BeFalse();
  }

  [Fact]
  public async Task SaveUbicationAsync_ShouldReturnTrue_WhenUserExists()
  {
    var dto = TestUbicationFactory.CreateSavedUbicationDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
    this._ubicationRepoMock.Setup(r => r.SaveUbicationAsync(dto)).ReturnsAsync(true);

    var result = await this._ubicationService.SaveUbicationAsync(dto).ConfigureAwait(false);

    result.Should().BeTrue();
  }

  [Fact]
  public async Task DeleteUbication_ShouldReturnFalse_WhenUserNotFound()
  {
    var dto = TestUbicationFactory.CreateUbicationInfoDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto?)null);

    var result = await this._ubicationService.DeleteUbication(dto).ConfigureAwait(false);

    result.Should().BeFalse();
  }

  [Fact]
  public async Task DeleteUbication_ShouldReturnTrue_WhenUserExists()
  {
    var dto = TestUbicationFactory.CreateUbicationInfoDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
    this._ubicationRepoMock.Setup(r => r.DeleteUbication(dto)).ReturnsAsync(true);

    var result = await this._ubicationService.DeleteUbication(dto).ConfigureAwait(false);

    result.Should().BeTrue();
  }

  [Fact]
  public async Task UpdateUbication_ShouldReturnFalse_WhenUserNotFound()
  {
    var dto = TestUbicationFactory.CreateUbicationInfoDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync((UserDto?)null);

    var result = await this._ubicationService.UpdateUbication(dto).ConfigureAwait(false);

    result.Should().BeFalse();
  }

  [Fact]
  public async Task UpdateUbication_ShouldReturnTrue_WhenUserExists()
  {
    var dto = TestUbicationFactory.CreateUbicationInfoDto();
    this._userRepoMock.Setup(r => r.GetUserByEmailAsync(dto.UserEmail)).ReturnsAsync(TestUserFactory.CreateValidUserDto());
    this._ubicationRepoMock.Setup(r => r.UpdateUbication(dto)).ReturnsAsync(true);

    var result = await this._ubicationService.UpdateUbication(dto).ConfigureAwait(false);

    result.Should().BeTrue();
  }
}
