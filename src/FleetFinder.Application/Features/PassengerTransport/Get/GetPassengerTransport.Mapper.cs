using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;
using PassengerImageEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransportImage;


namespace FleetFinder.Application.Features.PassengerTransport.Get;

public static partial class GetPassengerTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<PassengerEntity, ResponseDto>
    {
        [MapProperty(nameof(PassengerEntity.User), nameof(ResponseDto.Contact))]
        [MapperIgnoreSource(nameof(PassengerEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(PassengerEntity.State))]
        public partial ResponseDto Map(PassengerEntity source);

        private List<string> Map(List<PassengerImageEntity> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
