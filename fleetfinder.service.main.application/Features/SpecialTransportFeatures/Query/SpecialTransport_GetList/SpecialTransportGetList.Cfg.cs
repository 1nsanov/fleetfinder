using fleetfinder.service.main.application.Common.FeatureModels;
using fleetfinder.service.main.domain.Transport.Special;
using fleetfinder.service.main.domain.Users;
using Riok.Mapperly.Abstractions;

namespace fleetfinder.service.main.application.Features.SpecialTransportFeatures.Query.SpecialTransport_GetList;

public static partial class SpecialTransportGetList
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
    partial class Mapping : IMapCodeGen<List<SpecialTransport>, List<SpecialTransportDto>>
    {
        public partial List<SpecialTransportDto> Map(List<SpecialTransport> source);

        [MapProperty(nameof(SpecialTransport.User), nameof(SpecialTransportDto.Contact))]
        public partial SpecialTransportDto Map(SpecialTransport source);

        private List<string> Map(List<SpecialTransportImage> source) 
            => source.Select(x => x.Url).ToList();
        
        private ContactDto Map(User source)
            => new(source.Organization ?? $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode);
    }
}
