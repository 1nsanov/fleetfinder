using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.PassengerTransportFeatures.Query.PassengerTransport_GetList;

public static partial class PassengerTransportGetList
{
    #region Validator

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.PageSize).InclusiveBetween(6, 20);
            RuleFor(x => x.SkipCount).InclusiveBetween(0, int.MaxValue);
        }
    }

    #endregion
    
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<List<PassengerTransport>, List<PassengerTransportDto>>
    {
        public partial List<PassengerTransportDto> Map(List<PassengerTransport> source);

        [MapProperty(nameof(PassengerTransport.User), nameof(PassengerTransportDto.Contact))]
        [MapperIgnoreSource(nameof(PassengerTransport.RentalDuration))]
        [MapperIgnoreSource(nameof(PassengerTransport.CountSeats))]
        [MapperIgnoreSource(nameof(PassengerTransport.Size))]
        [MapperIgnoreSource(nameof(PassengerTransport.Option))]
        [MapperIgnoreSource(nameof(PassengerTransport.Color))]
        [MapperIgnoreSource(nameof(PassengerTransport.MinOrderTime))]
        [MapperIgnoreSource(nameof(PassengerTransport.Brand))]
        [MapperIgnoreSource(nameof(PassengerTransport.YearIssue))]
        [MapperIgnoreSource(nameof(PassengerTransport.ExperienceWork))]
        [MapperIgnoreSource(nameof(PassengerTransport.PaymentMethod))]
        [MapperIgnoreSource(nameof(PassengerTransport.PaymentOrder))]
        [MapperIgnoreSource(nameof(PassengerTransport.UserId))]
        [MapperIgnoreSource(nameof(PassengerTransport.CreateDate))]
        [MapperIgnoreSource(nameof(PassengerTransport.UpdateDate))]
        [MapperIgnoreSource(nameof(PassengerTransport.State))]
        public partial PassengerTransportDto Map(PassengerTransport source);

        private List<string> Map(List<PassengerTransportImage> source) 
            => source.Select(x => x.Url).ToList();
        
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
