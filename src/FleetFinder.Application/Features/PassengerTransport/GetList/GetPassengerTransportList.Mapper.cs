using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using PassengerEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransport;
using PassengerImageEntity = FleetFinder.Domain.Transport.Passenger.PassengerTransportImage;


namespace FleetFinder.Application.Features.PassengerTransport.GetList;

public static partial class GetPassengerTransportList
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<List<PassengerEntity>, List<PassengerTransportDto>>
    {
        public partial List<PassengerTransportDto> Map(List<PassengerEntity> source);

        [MapProperty(nameof(PassengerEntity.User), nameof(PassengerTransportDto.Contact))]
        [MapperIgnoreSource(nameof(PassengerEntity.RentalDuration))]
        [MapperIgnoreSource(nameof(PassengerEntity.CountSeats))]
        [MapperIgnoreSource(nameof(PassengerEntity.Size))]
        [MapperIgnoreSource(nameof(PassengerEntity.Option))]
        [MapperIgnoreSource(nameof(PassengerEntity.Color))]
        [MapperIgnoreSource(nameof(PassengerEntity.MinOrderTime))]
        [MapperIgnoreSource(nameof(PassengerEntity.Brand))]
        [MapperIgnoreSource(nameof(PassengerEntity.YearIssue))]
        [MapperIgnoreSource(nameof(PassengerEntity.ExperienceWork))]
        [MapperIgnoreSource(nameof(PassengerEntity.PaymentMethod))]
        [MapperIgnoreSource(nameof(PassengerEntity.PaymentOrder))]
        [MapperIgnoreSource(nameof(PassengerEntity.UserId))]
        [MapperIgnoreSource(nameof(PassengerEntity.CreateDate))]
        [MapperIgnoreSource(nameof(PassengerEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(PassengerEntity.State))]
        public partial PassengerTransportDto Map(PassengerEntity source);

        private List<string> Map(List<PassengerImageEntity> source) 
            => source.Select(x => x.Url).ToList();
        
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
