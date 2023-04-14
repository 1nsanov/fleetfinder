namespace fleetfinder.service.main.application.Features.UserFeatures.Command.Identify_RefreshToken;

public static partial class IdentifyRefreshToken
{
    #region Validator

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(cmd => cmd.AccessToken).NotEmpty().Unless(cmd => cmd.AccessToken is null);
            RuleFor(cmd => cmd.RefreshToken).NotEmpty();
        }
    }

    #endregion
    
    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        }
    }

    #endregion
}