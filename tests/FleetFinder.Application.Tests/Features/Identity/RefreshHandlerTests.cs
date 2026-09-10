using FleetFinder.Application.Features.Identity.Logout;
using FleetFinder.Application.Features.Identity.Refresh;
using FleetFinder.Application.Tests.Fixtures;
using Microsoft.IdentityModel.Tokens;

namespace FleetFinder.Application.Tests.Features.Identity;

public class RefreshHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public RefreshHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Handle_ReturnsNewPair_WhenTokensAreValid()
    {
        await using var db = _fx.CreateDb();
        var user = await _fx.SeedUserAsync(db, "demo", "Demo123!");
        var identity = _fx.CreateRealIdentityService(db);
        var issued = identity.GenerateTokenUser(user);
        await db.SaveChangesAsync();
        var handler = new Refresh.Handler(identity, db);

        var result = await handler.Handle(
            new Refresh.Command(issued.Access, issued.Refresh),
            CancellationToken.None);

        result.Token.Should().NotBeNull();
        result.Token!.Access.Should().NotBeNullOrEmpty();
        result.Token.Refresh.Should().NotBeNullOrEmpty();
        result.Token.Refresh.Should().NotBe(issued.Refresh);
    }

    [Fact]
    public async Task Handle_Throws_WhenRefreshTokenIsWrong()
    {
        await using var db = _fx.CreateDb();
        var user = await _fx.SeedUserAsync(db, "demo", "Demo123!");
        var identity = _fx.CreateRealIdentityService(db);
        var issued = identity.GenerateTokenUser(user);
        await db.SaveChangesAsync();
        var handler = new Refresh.Handler(identity, db);

        var act = () => handler.Handle(
            new Refresh.Command(issued.Access, "foreign-refresh-token"),
            CancellationToken.None);

        await act.Should().ThrowAsync<SecurityTokenValidationException>();
    }

    [Fact]
    public async Task Handle_Throws_AfterLogout()
    {
        await using var db = _fx.CreateDb();
        var user = await _fx.SeedUserAsync(db, "demo", "Demo123!");
        var identity = _fx.CreateRealIdentityService(db);
        var issued = identity.GenerateTokenUser(user);
        await db.SaveChangesAsync();
        await new Logout.Handler(identity, db).Handle(
            new Logout.Command(issued.Access),
            CancellationToken.None);
        var refresh = new Refresh.Handler(identity, db);

        var act = () => refresh.Handle(
            new Refresh.Command(issued.Access, issued.Refresh),
            CancellationToken.None);

        await act.Should().ThrowAsync<SecurityTokenValidationException>();
    }
}
