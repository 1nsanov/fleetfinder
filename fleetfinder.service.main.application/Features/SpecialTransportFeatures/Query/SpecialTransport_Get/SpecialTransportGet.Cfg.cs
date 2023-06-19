using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Special;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_Get;

public static partial class SpecialTransportGet
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<SpecialTransport, ResponseDto>
    {
        [MapProperty(nameof(SpecialTransport.User), nameof(ResponseDto.Contact))]
        public partial ResponseDto Map(SpecialTransport source);

        private List<string> Map(List<SpecialTransportImage> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
