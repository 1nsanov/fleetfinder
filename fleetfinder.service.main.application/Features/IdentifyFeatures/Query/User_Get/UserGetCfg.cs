using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.IdentifyFeatures.Query.User_Get;

public static partial class UserGet
{
    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ResponseDto>()
                .MapRecordMember(dest => dest.Username, src => src.Login);
        }
    }

    #endregion
}