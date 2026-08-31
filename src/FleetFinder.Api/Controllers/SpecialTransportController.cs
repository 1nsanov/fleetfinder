using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.SpecialTransport.Create;
using FleetFinder.Application.Features.SpecialTransport.Delete;
using FleetFinder.Application.Features.SpecialTransport.Get;
using FleetFinder.Application.Features.SpecialTransport.GetList;
using FleetFinder.Application.Features.SpecialTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

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
    public async Task<CreateSpecialTransport.ResponseDto> CreateSpecialTransport(CreateSpecialTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateSpecialTransport.Command(UserId, request), cancellationToken);
    }

    [Authorize]
    [HttpPut]
    public async Task<UpdateSpecialTransport.ResponseDto> UpdateSpecialTransport(UpdateSpecialTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateSpecialTransport.Command(UserId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<GetSpecialTransport.ResponseDto> GetSpecialTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetSpecialTransport.Query(id), cancellationToken);
    }

    [Authorize]
    [HttpDelete]
    public async Task<DeleteSpecialTransport.ResponseDto> DeleteSpecialTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeleteSpecialTransport.Command(UserId, id), cancellationToken);
    }

    [HttpGet("list")]
    public async Task<GetSpecialTransportList.ResponseDto> GetSpecialTransportList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] TransportSortParameter sortParameter = TransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] GetSpecialTransportList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new GetSpecialTransportList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}
