using System.Reflection;
using FleetFinder.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace FleetFinder.Application.Tests.Authorization;

public class ImageControllerAuthorizeTests
{
    [Fact]
    public void Controller_RequiresAuthorization()
    {
        typeof(ImageController).GetCustomAttribute<AuthorizeAttribute>().Should().NotBeNull();
        typeof(ImageController).Should().BeAssignableTo<HeadersController>();
    }
}
