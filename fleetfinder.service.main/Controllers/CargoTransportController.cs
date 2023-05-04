using fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CommandTransport_Post;

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

    /// <summary>
    /// Добавить груз. транспорт
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<CargoTransportPost.ResponseDto> CargoTransportPost(CargoTransportPost.RequestDto request, CancellationToken cancellationToken)
    {
        return  await _mediator.Send(new CargoTransportPost.Command(UserId, request), cancellationToken);
    }
}