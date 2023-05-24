using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Features.PassengerTransportFeatures.Command.PassengerTransport_Delete;
using fleetfinder.service.main.application.Features.PassengerTransportFeatures.Command.PassengerTransport_Post;
using fleetfinder.service.main.application.Features.PassengerTransportFeatures.Command.PassengerTransport_Put;
using fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_Get;
using fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_GetList;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/transport/passenger")]
public class PassengerTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public PassengerTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<PassengerTransportPost.ResponseDto> PassengerTransportPost(PassengerTransportPost.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PassengerTransportPost.Command(UserId, request), cancellationToken);
    }
    
    [Authorize]
    [HttpPut]
    public async  Task<PassengerTransportPut.ResponseDto> PassengerTransportPut(PassengerTransportPut.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PassengerTransportPut.Command(UserId, request), cancellationToken);
    }
    
    [HttpGet]
    public async Task<PassengerTransportGet.ResponseDto> PassengerTransportGet([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PassengerTransportGet.Query(id), cancellationToken);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<PassengerTransportDelete.ResponseDto> SpecialTransportDelete([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PassengerTransportDelete.Command(UserId, id), cancellationToken);
    }
    
    [HttpGet("list")]
    public async Task<PassengerTransportGetList.ResponseDto> SpecialTransportGetList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] TransportSortParameter sortParameter = TransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] PassengerTransportGetList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new PassengerTransportGetList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}