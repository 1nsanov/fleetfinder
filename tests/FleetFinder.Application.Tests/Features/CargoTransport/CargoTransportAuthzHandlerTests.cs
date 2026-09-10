using FleetFinder.Application.Abstractions;
using FleetFinder.Application.Common.Exceptions;
using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Application.Features.CargoTransport.Delete;
using FleetFinder.Application.Features.CargoTransport.Update;
using FleetFinder.Application.Tests.Fixtures;
using FleetFinder.Application.Tests.Infrastructure;
using FleetFinder.Domain.Bases;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport.Cargo;
using FleetFinder.Domain.Transport.Cargo;
using Microsoft.EntityFrameworkCore;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;

namespace FleetFinder.Application.Tests.Features.CargoTransport;

public class CargoTransportAuthzHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public CargoTransportAuthzHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Update_Throws_WhenUserDoesNotOwnListing()
    {
        await using var db = _fx.CreateDb();
        var listing = await SeedCargoAsync(db, ownerId: 1);
        var handler = new UpdateCargoTransport.Handler(db, Substitute.For<IMapper>());

        var act = () => handler.Handle(
            new UpdateCargoTransport.Command(2, UpdateRequest(listing.Id)),
            CancellationToken.None);

        await act.Should().ThrowAsync<EntityNotFoundException>();
        (await db.CargoTransport.SingleAsync()).Title.Should().Be("Owner truck");
    }

    [Fact]
    public async Task Delete_Throws_WhenUserDoesNotOwnListing()
    {
        await using var db = _fx.CreateDb();
        var listing = await SeedCargoAsync(db, ownerId: 1);
        var handler = new DeleteCargoTransport.Handler(db);

        var act = () => handler.Handle(
            new DeleteCargoTransport.Command(2, listing.Id),
            CancellationToken.None);

        await act.Should().ThrowAsync<EntityNotFoundException>();
        (await db.CargoTransport.SingleAsync()).State.Should().Be(State.Actual);
    }

    private static async Task<CargoEntity> SeedCargoAsync(TestCommandDbContext db, long ownerId)
    {
        var entity = new CargoEntity
        {
            Title = "Owner truck",
            Region = Region.Tiraspol,
            Type = CargoType.T1,
            UserId = ownerId,
            Price = new Price(),
            Body = new Body(),
            Images = [],
            State = State.Actual
        };
        db.CargoTransport.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    private static UpdateCargoTransport.RequestDto UpdateRequest(long id) => new(
        id,
        "Hacked title",
        Region.Tiraspol,
        "Volvo",
        "2020",
        null,
        null,
        null,
        new PriceDto(100, null, null),
        null,
        CargoType.T1,
        new BodyDto(null, null, null, null, null, null),
        null,
        []);
}
