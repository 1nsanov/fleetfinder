using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace FleetFinder.Application.Features.UserProfile.Update;

public static partial class UpdateUserProfile
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, User>
    {
        [MapperIgnoreTarget(nameof(User.Login))]
        [MapperIgnoreTarget(nameof(User.Password))]
        [MapperIgnoreTarget(nameof(User.RefreshToken))]
        [MapperIgnoreTarget(nameof(User.CargoTransports))]
        [MapperIgnoreTarget(nameof(User.PassengerTransports))]
        [MapperIgnoreTarget(nameof(User.SpecialTransports))]
        [MapperIgnoreTarget(nameof(User.Id))]
        [MapperIgnoreTarget(nameof(User.CreateDate))]
        [MapperIgnoreTarget(nameof(User.UpdateDate))]
        [MapperIgnoreTarget(nameof(User.State))]
        public partial User Map(RequestDto source);
    }
}
