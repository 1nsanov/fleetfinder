using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_Get;

public static partial class CargoTransportGet
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<CargoTransport, ResponseDto>
    {
        [MapProperty(nameof(CargoTransport.User), nameof(ResponseDto.Contact))]
        public partial ResponseDto Map(CargoTransport source);

        private List<string> Map(List<CargoTransportImage> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(source.Organization ?? $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode);
    }
}
