using FleetFinder.Application.Features.Identity.SignIn;
using FleetFinder.Application.Services;
using FleetFinder.Application.Tests.Fixtures;

namespace FleetFinder.Application.Tests.Features.Identity;

public class SignInHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public SignInHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Handle_ReturnsToken_WhenCredentialsAreValid()
    {
        await using var db = _fx.CreateDb();
        await _fx.SeedUserAsync(db, "demo", "Demo123!");
        var handler = new SignIn.Handler(
            _fx.CreateIdentityService(),
            new UserService(db, _fx.PasswordService),
            db);

        var result = await handler.Handle(
            new SignIn.Command(new SignIn.RequestDto("demo", "Demo123!")),
            CancellationToken.None);

        result.Token.Access.Should().NotBeNullOrEmpty();
        result.Token.Refresh.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_Throws_WhenPasswordIsInvalid()
    {
        await using var db = _fx.CreateDb();
        await _fx.SeedUserAsync(db, "demo", "Demo123!");
        var handler = new SignIn.Handler(
            _fx.CreateIdentityService(),
            new UserService(db, _fx.PasswordService),
            db);

        var act = () => handler.Handle(
            new SignIn.Command(new SignIn.RequestDto("demo", "WrongPass1")),
            CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
