using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.CargoTransportFeatures.Query.CargoTransport_GetList;

public static partial class CargoTransportGetList
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
    partial class Mapping : IMapCodeGen<List<CargoTransport>, List<CargoTransportDto>>
    {
        public partial List<CargoTransportDto> Map(List<CargoTransport> source);

        [MapProperty(nameof(CargoTransport.User), nameof(CargoTransportDto.Contact))]
        public partial CargoTransportDto Map(CargoTransport source);

        private List<string> Map(List<CargoTransportImage> source) 
            => source.Select(x => x.Url).ToList();
        
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
