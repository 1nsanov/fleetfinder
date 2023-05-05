using fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Post;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_Get;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/cargo/transport")]
public class CargoTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public CargoTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CargoTransportPost.ResponseDto> CargoTransportPost(CargoTransportPost.RequestDto request, CancellationToken cancellationToken)
    {
        return  await _mediator.Send(new CargoTransportPost.Command(UserId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<CargoTransportGet.ResponseDto> CargoTransportGet([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CargoTransportGet.Query(UserId, id), cancellationToken);
    }
}