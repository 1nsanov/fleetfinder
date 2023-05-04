namespace fleetfinder.service.main.Controllers;


public abstract class HeadersController
{
    [FromHeader]
    public long UserId { get; set; }
}