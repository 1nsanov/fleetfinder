using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Features.SpecialTransportFeatures.Command.SpecialTransport_Delete;
using fleetfinder.service.main.application.Features.SpecialTransportFeatures.Command.SpecialTransport_Post;
using fleetfinder.service.main.application.Features.SpecialTransportFeatures.Command.SpecialTransport_Put;
using fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_Get;
using fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_GetList;
using Microsoft.AspNetCore.Authorization;

namespace fleetfinder.service.main.Controllers;

[ApiController]
[Route("api/transport/special")]
public class SpecialTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public SpecialTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<SpecialTransportPost.ResponseDto> SpecialTransportPost(SpecialTransportPost.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SpecialTransportPost.Command(UserId, request), cancellationToken);
    }
    
    [Authorize]
    [HttpPut]
    public async  Task<SpecialTransportPut.ResponseDto> SpecialTransportPut(SpecialTransportPut.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SpecialTransportPut.Command(UserId, request), cancellationToken);
    }
    
    [HttpGet]
    public async Task<SpecialTransportGet.ResponseDto> SpecialTransportGet([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SpecialTransportGet.Query(id), cancellationToken);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<SpecialTransportDelete.ResponseDto> SpecialTransportDelete([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new SpecialTransportDelete.Command(UserId, id), cancellationToken);
    }
    
    [HttpGet("list")]
    public async Task<SpecialTransportGetList.ResponseDto> SpecialTransportGetList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] TransportSortParameter sortParameter = TransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] SpecialTransportGetList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new SpecialTransportGetList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}