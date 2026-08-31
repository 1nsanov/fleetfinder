using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace FleetFinder.Application.Features.UserProfile.Get;

public static partial class GetUserProfile
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
