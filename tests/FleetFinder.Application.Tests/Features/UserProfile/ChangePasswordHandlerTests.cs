using FleetFinder.Application.Features.UserProfile.ChangePassword;
using FleetFinder.Application.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Tests.Features.UserProfile;

public class ChangePasswordHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public ChangePasswordHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Handle_Throws_WhenCurrentPasswordIsWrong()
    {
        await using var db = _fx.CreateDb();
        var user = await _fx.SeedUserAsync(db, "demo", "OldPass12");
        var handler = new ChangePassword.Handler(db, _fx.PasswordService);

        var act = () => handler.Handle(
            new ChangePassword.Command(user.Id, new ChangePassword.RequestDto("wrong-pass", "NewPass12")),
            CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>().WithMessage("*incorrect*");
    }

    [Fact]
    public async Task Handle_UpdatesHash_WhenCurrentPasswordIsValid()
    {
        await using var db = _fx.CreateDb();
        var user = await _fx.SeedUserAsync(db, "demo", "OldPass12");
        var handler = new ChangePassword.Handler(db, _fx.PasswordService);

        var result = await handler.Handle(
            new ChangePassword.Command(user.Id, new ChangePassword.RequestDto("OldPass12", "NewPass12")),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        var saved = await db.User.SingleAsync(u => u.Id == user.Id);
        _fx.PasswordService.VerifyPassword("NewPass12", saved.Password).Should().BeTrue();
        _fx.PasswordService.VerifyPassword("OldPass12", saved.Password).Should().BeFalse();
    }
}
