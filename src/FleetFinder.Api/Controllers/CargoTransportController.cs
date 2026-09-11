using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.CargoTransport.Create;
using FleetFinder.Application.Features.CargoTransport.Delete;
using FleetFinder.Application.Features.CargoTransport.Get;
using FleetFinder.Application.Features.CargoTransport.GetList;
using FleetFinder.Application.Features.CargoTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Cargo transport listings.
/// </summary>
[ApiController]
[Route("api/transport/cargo")]
public class CargoTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public CargoTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a cargo listing for the authenticated user.
    /// </summary>
    /// <param name="request">Listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the created listing.</returns>
    [Authorize]
    [HttpPost]
    public async Task<CreateCargoTransport.ResponseDto> CreateCargoTransport(CreateCargoTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateCargoTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Updates a cargo listing owned by the authenticated user.
    /// </summary>
    /// <param name="request">Updated listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the updated listing.</returns>
    [Authorize]
    [HttpPut]
    public async Task<UpdateCargoTransport.ResponseDto> UpdateCargoTransport(UpdateCargoTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateCargoTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Returns a cargo listing by id.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Listing details.</returns>
    [HttpGet]
    public async Task<GetCargoTransport.ResponseDto> GetCargoTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetCargoTransport.Query(id), cancellationToken);
    }

    /// <summary>
    /// Deletes a cargo listing owned by the authenticated user.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the deleted listing.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<DeleteCargoTransport.ResponseDto> DeleteCargoTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeleteCargoTransport.Command(UserId, id), cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of cargo listings.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="pageSize">Page size (6–20).</param>
    /// <param name="skipCount">Number of items to skip.</param>
    /// <param name="sortParameter">Sort field.</param>
    /// <param name="sortDesc">Sort descending when <c>true</c>.</param>
    /// <param name="requestFilter">Optional filters.</param>
    /// <returns>Paged listing collection.</returns>
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
