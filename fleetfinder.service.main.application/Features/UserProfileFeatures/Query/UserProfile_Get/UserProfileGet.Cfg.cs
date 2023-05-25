using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Query.UserProfile_Get;

public static partial class UserProfileGet
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<User, ResponseDto>
    {
        public partial ResponseDto Map(User source);
    }
}
