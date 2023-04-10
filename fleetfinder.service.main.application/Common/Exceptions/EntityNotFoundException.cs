using System.Runtime.CompilerServices;

namespace fleetfinder.service.main.application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(long? id, [CallerArgumentExpression("id")] string paramName = "")
        : base($"Entity with id {id} not found. ParamName: {paramName}")
    {
    }

    public EntityNotFoundException(string? value, [CallerArgumentExpression("value")] string paramName = "")
        : base($"Entity with value {value} not found. ParamName: {paramName}")
    {
    }

    public EntityNotFoundException()
        : base()
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}