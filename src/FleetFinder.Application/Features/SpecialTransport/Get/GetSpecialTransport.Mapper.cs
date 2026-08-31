using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Special;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using SpecialEntity = FleetFinder.Domain.Transport.Special.SpecialTransport;
using SpecialImageEntity = FleetFinder.Domain.Transport.Special.SpecialTransportImage;


namespace FleetFinder.Application.Features.SpecialTransport.Get;

public static partial class GetSpecialTransport
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<SpecialEntity, ResponseDto>
    {
        [MapProperty(nameof(SpecialEntity.User), nameof(ResponseDto.Contact))]
        [MapperIgnoreSource(nameof(SpecialEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(SpecialEntity.State))]
        public partial ResponseDto Map(SpecialEntity source);

        private List<string> Map(List<SpecialImageEntity> source) 
            => source.Select(x => x.Url).ToList(); 
    
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
