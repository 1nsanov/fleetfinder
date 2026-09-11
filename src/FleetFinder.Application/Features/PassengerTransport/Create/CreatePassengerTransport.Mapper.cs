using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Passenger;
using Riok.Mapperly.Abstractions;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;
using PassengerImageEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransportImage;


namespace FleetFinder.Application.Features.PassengerTransport.Create;

public static partial class CreatePassengerTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, PassengerEntity>
    {
        [MapperIgnoreTarget(nameof(PassengerEntity.User))]
        [MapperIgnoreTarget(nameof(PassengerEntity.UserId))]
        [MapperIgnoreTarget(nameof(PassengerEntity.Id))]
        [MapperIgnoreTarget(nameof(PassengerEntity.CreateDate))]
        [MapperIgnoreTarget(nameof(PassengerEntity.UpdateDate))]
        [MapperIgnoreTarget(nameof(PassengerEntity.State))]
        public partial PassengerEntity Map(RequestDto source);

        private List<PassengerImageEntity> Map(List<string> source)
            => source.ConvertAll(img => new PassengerImageEntity { Url = img });
    }
}
