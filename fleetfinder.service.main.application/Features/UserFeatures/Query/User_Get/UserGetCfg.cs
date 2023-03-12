using fleetfinder.service.main.domain.Users;

namespace fleetfinder.service.main.application.Features.UserFeatures.Query.User_Get;

public static partial class UserGet
{
    #region Mapper

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ResponseDto>()
                .ForMember(dest => dest.Username, 
                    opt => opt.MapFrom(src => src.Login));
        }
    }

    #endregion
}