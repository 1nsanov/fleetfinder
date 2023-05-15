using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Delete;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Post;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Command.CargoTransport_Put;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_Get;
using fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_GetList;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpPost]
    public async Task<CargoTransportPost.ResponseDto> CargoTransportPost(CargoTransportPost.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CargoTransportPost.Command(UserId, request), cancellationToken);
    }
    
    [Authorize]
    [HttpPut]
    public async  Task<CargoTransportPut.ResponseDto> CargoTransportPut(CargoTransportPut.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CargoTransportPut.Command(UserId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<CargoTransportGet.ResponseDto> CargoTransportGet([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CargoTransportGet.Query(id), cancellationToken);
    }

    [Authorize]
    [HttpDelete]
    public async Task<CargoTransportDelete.ResponseDto> CargoTransportDelete([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CargoTransportDelete.Command(UserId, id), cancellationToken);
    }

    [HttpGet("list")]
    public async Task<CargoTransportGetList.ResponseDto> CargoTransportGetList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] CargoTransportSortParameter sortParameter = CargoTransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] CargoTransportGetList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new CargoTransportGetList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}