using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.CargoTransport.Create;
using FleetFinder.Application.Features.CargoTransport.Delete;
using FleetFinder.Application.Features.CargoTransport.Get;
using FleetFinder.Application.Features.CargoTransport.GetList;
using FleetFinder.Application.Features.CargoTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

[ApiController]
[Route("api/transport/cargo")]
public class CargoTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public CargoTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<CreateCargoTransport.ResponseDto> CreateCargoTransport(CreateCargoTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateCargoTransport.Command(UserId, request), cancellationToken);
    }

    [Authorize]
    [HttpPut]
    public async Task<UpdateCargoTransport.ResponseDto> UpdateCargoTransport(UpdateCargoTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateCargoTransport.Command(UserId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<GetCargoTransport.ResponseDto> GetCargoTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetCargoTransport.Query(id), cancellationToken);
    }

    [Authorize]
    [HttpDelete]
    public async Task<DeleteCargoTransport.ResponseDto> DeleteCargoTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeleteCargoTransport.Command(UserId, id), cancellationToken);
    }

    [HttpGet("list")]
    public async Task<GetCargoTransportList.ResponseDto> GetCargoTransportList(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize = 6,
        [FromQuery] int skipCount = 0,
        [FromQuery] TransportSortParameter sortParameter = TransportSortParameter.Default,
        [FromQuery] bool sortDesc = false,
        [FromQuery] GetCargoTransportList.RequestFilter? requestFilter = null)
    {
        return await _mediator.Send(new GetCargoTransportList.Query(pageSize, skipCount, sortParameter, sortDesc, requestFilter), cancellationToken);
    }
}
