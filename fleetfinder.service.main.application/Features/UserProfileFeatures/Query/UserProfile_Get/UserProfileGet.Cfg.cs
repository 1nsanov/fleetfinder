using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.UserProfileFeatures.Query.UserProfile_Get;

public static partial class UserProfileGet
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<User, ResponseDto>
    {
        [MapperIgnoreSource(nameof(User.Password))]
        [MapperIgnoreSource(nameof(User.RefreshToken))]
        [MapperIgnoreSource(nameof(User.CargoTransports))]
        [MapperIgnoreSource(nameof(User.PassengerTransports))]
        [MapperIgnoreSource(nameof(User.SpecialTransports))]
        [MapperIgnoreSource(nameof(User.Id))]
        [MapperIgnoreSource(nameof(User.CreateDate))]
        [MapperIgnoreSource(nameof(User.UpdateDate))]
        [MapperIgnoreSource(nameof(User.State))]
        public partial ResponseDto Map(User source);
    }
}
