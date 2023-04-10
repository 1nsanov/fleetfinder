using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.UserFeatures.Command.User_SignUp;

public static partial class UserSignUp
{
    #region Validator

    internal class Validator : AbstractValidator<RequestDto>
    {
        public Validator()
        {
            RuleFor(dto => dto.Login).NotEmpty().MaximumLength(16);
        }
    }

    #endregion

    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RequestDto, User>()
                .MapRecordMember(dest => dest.FullName, src => $"{src.Name.First} {src.Name.Last} {src.Name.Middle}");
        }
    }

    #endregion
}