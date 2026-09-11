using FleetFinder.Application.Abstractions;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Persistence;
using FleetFinder.Application.Common.Options;
using FleetFinder.Application.Services;
using FleetFinder.Application.Services.Models;
using FleetFinder.Application.Tests.Infrastructure;
using FleetFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FleetFinder.Application.Tests.Fixtures;

public sealed class HandlerTestFixture : IAsyncLifetime
{
    public IPasswordService PasswordService { get; private set; } = null!;

    public Task InitializeAsync()
    {
        PasswordService = new PasswordService(Options.Create(new PasswordOptions
        {
            MemorySize = 1024,
            Iterations = 1,
            Parallelism = 1,
            SaltSize = 16,
            HashSize = 32
        }));
        return Task.CompletedTask;
    }

    public Task DisposeAsync() => Task.CompletedTask;

    public TestCommandDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<TestCommandDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var db = new TestCommandDbContext(options);
        db.Database.EnsureCreated();
        return db;
    }

    public async Task<User> SeedUserAsync(TestCommandDbContext db, string login, string password)
    {
        var user = new User
        {
            Login = login,
            Password = PasswordService.HashPassword(password),
            Email = $"{login}@example.com",
            FullName = new FullName { First = "Test", Second = "User" },
            Contact = new Contact()
        };
        db.User.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public IIdentityService CreateIdentityService(TokenDto? token = null)
    {
        var identity = Substitute.For<IIdentityService>();
        identity.GenerateTokenUser(Arg.Any<User>()).Returns(token ?? new TokenDto
        {
            Access = "access-token",
            Refresh = "refresh-token",
            ExpiryTime = DateTime.UtcNow.AddMinutes(15)
        });
        return identity;
    }

    public IdentityService CreateRealIdentityService(IQueryDbContext db)
    {
        return new IdentityService(db, Options.Create(new JwtOptions
        {
            Key = "integration-test-jwt-key-32bytes!",
            Issuer = "FleetFinder.Tests",
            Audience = "FleetFinder.Tests",
            AccessTokenLifetimeMinutes = 15,
            RefreshTokenLifetimeDays = 7
        }));
    }

    public IMapper CreateMapper<TSource, TDestination>(Func<TSource, TDestination> map)
        where TSource : notnull
        where TDestination : notnull
    {
        var mapper = Substitute.For<IMapper>();
        mapper.Map<TSource, TDestination>(Arg.Any<TSource>())
            .Returns(call => map(call.Arg<TSource>()));
        return mapper;
    }
}
