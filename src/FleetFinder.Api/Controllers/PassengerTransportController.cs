using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.PassengerTransport.Create;
using FleetFinder.Application.Features.PassengerTransport.Delete;
using FleetFinder.Application.Features.PassengerTransport.Get;
using FleetFinder.Application.Features.PassengerTransport.GetList;
using FleetFinder.Application.Features.PassengerTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Passenger transport listings.
/// </summary>
[ApiController]
[Route("api/transport/passenger")]
public class PassengerTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public PassengerTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a passenger listing for the authenticated user.
    /// </summary>
    /// <param name="request">Listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the created listing.</returns>
    [Authorize]
    [HttpPost]
    public async Task<CreatePassengerTransport.ResponseDto> CreatePassengerTransport(CreatePassengerTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreatePassengerTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Updates a passenger listing owned by the authenticated user.
    /// </summary>
    /// <param name="request">Updated listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the updated listing.</returns>
    [Authorize]
    [HttpPut]
    public async Task<UpdatePassengerTransport.ResponseDto> UpdatePassengerTransport(UpdatePassengerTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdatePassengerTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Returns a passenger listing by id.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Listing details.</returns>
    [HttpGet]
    public async Task<GetPassengerTransport.ResponseDto> GetPassengerTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPassengerTransport.Query(id), cancellationToken);
    }

    /// <summary>
    /// Deletes a passenger listing owned by the authenticated user.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the deleted listing.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<DeletePassengerTransport.ResponseDto> DeletePassengerTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeletePassengerTransport.Command(UserId, id), cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of passenger listings.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="pageSize">Page size (6–20).</param>
    /// <param name="skipCount">Number of items to skip.</param>
    /// <param name="sortParameter">Sort field.</param>
    /// <param name="sortDesc">Sort descending when <c>true</c>.</param>
    /// <param name="requestFilter">Optional filters.</param>
    /// <returns>Paged listing collection.</returns>
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
