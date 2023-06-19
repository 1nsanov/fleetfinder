using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_Get;

public static partial class PassengerTransportGet
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<PassengerTransport, ResponseDto>
    {
        [MapProperty(nameof(PassengerTransport.User), nameof(ResponseDto.Contact))]
        public partial ResponseDto Map(PassengerTransport source);

        private List<string> Map(List<PassengerTransportImage> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
