using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Application.Features.CargoTransport.Create;
using FleetFinder.Application.Tests.Fixtures;
using FleetFinder.Domain.Bases;
using FleetFinder.Domain.Enums.Common;
using FleetFinder.Domain.Enums.Transport.Cargo;
using FleetFinder.Domain.Transport.Cargo;
using Microsoft.EntityFrameworkCore;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;

namespace FleetFinder.Application.Tests.Features.CargoTransport;

public class CreateCargoTransportHandlerTests : IClassFixture<HandlerTestFixture>
{
    private readonly HandlerTestFixture _fx;

    public CreateCargoTransportHandlerTests(HandlerTestFixture fx)
    {
        _fx = fx;
    }

    [Fact]
    public async Task Handle_PersistsEntity_WithUserId()
    {
        await using var db = _fx.CreateDb();
        const long userId = 42;
        var mapper = _fx.CreateMapper<CreateCargoTransport.RequestDto, CargoEntity>(dto =>
            new CargoEntity
            {
                Title = dto.Title,
                Region = dto.Region,
                Type = dto.Type,
                Price = new Price(),
                Body = new Body(),
                Images = []
            });
        var handler = new CreateCargoTransport.Handler(db, mapper);
        var request = new CreateCargoTransport.RequestDto(
            "Dump truck",
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

        var result = await handler.Handle(new CreateCargoTransport.Command(userId, request), CancellationToken.None);

        result.Id.Should().BePositive();
        var entity = await db.CargoTransport.SingleAsync();
        entity.UserId.Should().Be(userId);
        entity.Title.Should().Be("Dump truck");
    }
}
