namespace FleetFinder.Application.Abstractions;

public interface IQueryRequest<out TResponse> : IRequest<TResponse>
{
    
}