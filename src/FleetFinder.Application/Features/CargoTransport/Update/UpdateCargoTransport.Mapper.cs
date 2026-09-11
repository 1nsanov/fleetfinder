using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Cargo;
using Riok.Mapperly.Abstractions;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;
using CargoImageEntity = FleetFinder.Domain.Transport.Cargo.CargoTransportImage;


namespace FleetFinder.Application.Features.CargoTransport.Update;

public static partial class UpdateCargoTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, CargoEntity>
    {
        [MapperIgnoreTarget(nameof(CargoEntity.User))]
        [MapperIgnoreTarget(nameof(CargoEntity.UserId))]
        [MapperIgnoreTarget(nameof(CargoEntity.CreateDate))]
        [MapperIgnoreTarget(nameof(CargoEntity.UpdateDate))]
        [MapperIgnoreTarget(nameof(CargoEntity.State))]
        public partial CargoEntity Map(RequestDto source);

        private List<CargoImageEntity> Map(List<string> source)
            => source.ConvertAll(img => new CargoImageEntity { Url = img });
    }
}
