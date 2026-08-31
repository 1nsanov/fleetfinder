using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Special;
using Riok.Mapperly.Abstractions;
using SpecialEntity = FleetFinder.Domain.Transport.Special.SpecialTransport;
using SpecialImageEntity = FleetFinder.Domain.Transport.Special.SpecialTransportImage;


namespace FleetFinder.Application.Features.SpecialTransport.Create;

public static partial class CreateSpecialTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<RequestDto, SpecialEntity>
    {
        [MapperIgnoreTarget(nameof(SpecialEntity.User))]
        [MapperIgnoreTarget(nameof(SpecialEntity.UserId))]
        [MapperIgnoreTarget(nameof(SpecialEntity.Id))]
        [MapperIgnoreTarget(nameof(SpecialEntity.CreateDate))]
        [MapperIgnoreTarget(nameof(SpecialEntity.UpdateDate))]
        [MapperIgnoreTarget(nameof(SpecialEntity.State))]
        public partial SpecialEntity Map(RequestDto source);

        private List<SpecialImageEntity> Map(List<string> source)
            => source.ConvertAll(img => new SpecialImageEntity { Url = img });
    }
}
