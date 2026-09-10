using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.SpecialTransport.Create;
using FleetFinder.Application.Features.SpecialTransport.Delete;
using FleetFinder.Application.Features.SpecialTransport.Get;
using FleetFinder.Application.Features.SpecialTransport.GetList;
using FleetFinder.Application.Features.SpecialTransport.Update;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Api.Controllers;

/// <summary>
/// Special machinery listings.
/// </summary>
[ApiController]
[Route("api/transport/special")]
public class SpecialTransportController : HeadersController
{
    private readonly IMediator _mediator;

    public SpecialTransportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a special-machinery listing for the authenticated user.
    /// </summary>
    /// <param name="request">Listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the created listing.</returns>
    [Authorize]
    [HttpPost]
    public async Task<CreateSpecialTransport.ResponseDto> CreateSpecialTransport(CreateSpecialTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateSpecialTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Updates a special-machinery listing owned by the authenticated user.
    /// </summary>
    /// <param name="request">Updated listing payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the updated listing.</returns>
    [Authorize]
    [HttpPut]
    public async Task<UpdateSpecialTransport.ResponseDto> UpdateSpecialTransport(UpdateSpecialTransport.RequestDto request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new UpdateSpecialTransport.Command(UserId, request), cancellationToken);
    }

    /// <summary>
    /// Returns a special-machinery listing by id.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Listing details.</returns>
    [HttpGet]
    public async Task<GetSpecialTransport.ResponseDto> GetSpecialTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetSpecialTransport.Query(id), cancellationToken);
    }

    /// <summary>
    /// Deletes a special-machinery listing owned by the authenticated user.
    /// </summary>
    /// <param name="id">Listing id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Id of the deleted listing.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<DeleteSpecialTransport.ResponseDto> DeleteSpecialTransport([FromQuery] long id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new DeleteSpecialTransport.Command(UserId, id), cancellationToken);
    }

    /// <summary>
    /// Returns a paginated list of special-machinery listings.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <param name="pageSize">Page size (6–20).</param>
    /// <param name="skipCount">Number of items to skip.</param>
    /// <param name="sortParameter">Sort field.</param>
    /// <param name="sortDesc">Sort descending when <c>true</c>.</param>
    /// <param name="requestFilter">Optional filters.</param>
    /// <returns>Paged listing collection.</returns>
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
