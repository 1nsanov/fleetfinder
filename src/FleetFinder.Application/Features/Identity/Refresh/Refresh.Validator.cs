namespace FleetFinder.Application.Features.Identity.Refresh;

public static partial class Refresh
{


    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(cmd => cmd.AccessToken).NotEmpty().Unless(cmd => cmd.AccessToken is null);
            RuleFor(cmd => cmd.RefreshToken).NotEmpty();
        }
    }

    
}
