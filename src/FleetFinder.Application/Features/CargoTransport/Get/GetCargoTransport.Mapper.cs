using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;
using CargoImageEntity = FleetFinder.Domain.Transport.Cargo.CargoTransportImage;


namespace FleetFinder.Application.Features.CargoTransport.Get;

public static partial class GetCargoTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<CargoEntity, ResponseDto>
    {
        [MapProperty(nameof(CargoEntity.User), nameof(ResponseDto.Contact))]
        [MapperIgnoreSource(nameof(CargoEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(CargoEntity.State))]
        public partial ResponseDto Map(CargoEntity source);

        private List<string> Map(List<CargoImageEntity> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
