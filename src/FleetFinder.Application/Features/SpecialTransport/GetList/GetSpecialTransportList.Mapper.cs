using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Special;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using SpecialEntity = FleetFinder.Domain.Transport.Special.SpecialTransport;
using SpecialImageEntity = FleetFinder.Domain.Transport.Special.SpecialTransportImage;


namespace FleetFinder.Application.Features.SpecialTransport.GetList;

public static partial class GetSpecialTransportList
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<List<SpecialEntity>, List<SpecialTransportDto>>
    {
        public partial List<SpecialTransportDto> Map(List<SpecialEntity> source);

        [MapProperty(nameof(SpecialEntity.User), nameof(SpecialTransportDto.Contact))]
        [MapperIgnoreSource(nameof(SpecialEntity.Brand))]
        [MapperIgnoreSource(nameof(SpecialEntity.YearIssue))]
        [MapperIgnoreSource(nameof(SpecialEntity.ExperienceWork))]
        [MapperIgnoreSource(nameof(SpecialEntity.PaymentMethod))]
        [MapperIgnoreSource(nameof(SpecialEntity.PaymentOrder))]
        [MapperIgnoreSource(nameof(SpecialEntity.UserId))]
        [MapperIgnoreSource(nameof(SpecialEntity.CreateDate))]
        [MapperIgnoreSource(nameof(SpecialEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(SpecialEntity.State))]
        public partial SpecialTransportDto Map(SpecialEntity source);

        private List<string> Map(List<SpecialImageEntity> source)
            => source.Select(x => x.Url).ToList();

        private ContactDto Map(User source)
            => new(
                !string.IsNullOrEmpty(source.Organization)
                    ? source.Organization
                    : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}",
                source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode,
                source.ImageUrl);
    }
}
