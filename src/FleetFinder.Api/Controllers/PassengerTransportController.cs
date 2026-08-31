using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.PassengerTransport.Create;
using FleetFinder.Application.Features.PassengerTransport.Delete;
using FleetFinder.Application.Features.PassengerTransport.Get;
using FleetFinder.Application.Features.PassengerTransport.GetList;
using FleetFinder.Application.Features.PassengerTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

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
    public async Task<CreatePassengerTransport.ResponseDto> CreatePassengerTransport(CreatePassengerTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreatePassengerTransport.Command(UserId, request), cancellationToken);
    }

    [Authorize]
    [HttpPut]
    public async Task<UpdatePassengerTransport.ResponseDto> UpdatePassengerTransport(UpdatePassengerTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdatePassengerTransport.Command(UserId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<GetPassengerTransport.ResponseDto> GetPassengerTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPassengerTransport.Query(id), cancellationToken);
    }

    [Authorize]
    [HttpDelete]
    public async Task<DeletePassengerTransport.ResponseDto> DeletePassengerTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeletePassengerTransport.Command(UserId, id), cancellationToken);
    }

    [HttpGet("list")]
    public async Task<GetPassengerTransportList.ResponseDto> GetPassengerTransportList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] TransportSortParameter sortParameter = TransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] GetPassengerTransportList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new GetPassengerTransportList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}
