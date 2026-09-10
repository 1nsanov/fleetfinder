using FleetFinder.Application.Abstractions;
using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Application.Features.Identity.SignUp;
using FleetFinder.Application.Tests.Fixtures;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FleetFinder.Application.Tests.Features.Identity;

public class SignUpHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public SignUpHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Handle_CreatesUser_WhenLoginIsUnique()
    {
        await using var db = _fx.CreateDb();
        var mapper = _fx.CreateMapper<SignUp.RequestDto, User>(dto => new User
        {
            Login = dto.Login,
            Password = dto.Password,
            Email = dto.Email,
            FullName = new FullName { First = dto.FullName.First, Second = dto.FullName.Second },
            Contact = new Contact()
        });
        var handler = new SignUp.Handler(db, mapper, _fx.CreateIdentityService(), _fx.PasswordService);
        var request = new SignUp.RequestDto(
            "newuser",
            "Password1!",
            "user@example.com",
            new FullNameDto("Ann", "Smith", null),
            null);

        var result = await handler.Handle(new SignUp.Command(request), CancellationToken.None);

        result.Token.Access.Should().NotBeNullOrEmpty();
        var saved = await db.User.SingleAsync(u => u.Login == "newuser");
        saved.Password.Should().NotBe(request.Password);
        _fx.PasswordService.VerifyPassword(request.Password, saved.Password).Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Throws_WhenLoginAlreadyExists()
    {
        await using var db = _fx.CreateDb();
        await _fx.SeedUserAsync(db, "taken", "Password1!");
        var handler = new SignUp.Handler(
            db,
            Substitute.For<IMapper>(),
            _fx.CreateIdentityService(),
            _fx.PasswordService);
        var request = new SignUp.RequestDto(
            "taken",
            "Password1!",
            "user@example.com",
            new FullNameDto("Ann", "Smith", null),
            null);

        var act = () => handler.Handle(new SignUp.Command(request), CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>().WithMessage("*already exists*");
    }
}
