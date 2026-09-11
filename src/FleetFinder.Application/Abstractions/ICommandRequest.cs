namespace FleetFinder.Application.Abstractions;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>
{
    
}