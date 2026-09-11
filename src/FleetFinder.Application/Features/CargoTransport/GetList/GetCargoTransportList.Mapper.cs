using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Users;
using Riok.Mapperly.Abstractions;
using CargoEntity = FleetFinder.Domain.Transport.Cargo.CargoTransport;
using CargoImageEntity = FleetFinder.Domain.Transport.Cargo.CargoTransportImage;


namespace FleetFinder.Application.Features.CargoTransport.GetList;

public static partial class GetCargoTransportList
{
    [Mapper(PropertyNameMappingStrategy = PropertyNameMappingStrategy.CaseInsensitive)]
    partial class Mapping : IMapCodeGen<List<CargoEntity>, List<CargoTransportDto>>
    {
        public partial List<CargoTransportDto> Map(List<CargoEntity> source);

        [MapProperty(nameof(CargoEntity.User), nameof(CargoTransportDto.Contact))]
        [MapperIgnoreSource(nameof(CargoEntity.Body))]
        [MapperIgnoreSource(nameof(CargoEntity.Brand))]
        [MapperIgnoreSource(nameof(CargoEntity.YearIssue))]
        [MapperIgnoreSource(nameof(CargoEntity.ExperienceWork))]
        [MapperIgnoreSource(nameof(CargoEntity.PaymentMethod))]
        [MapperIgnoreSource(nameof(CargoEntity.PaymentOrder))]
        [MapperIgnoreSource(nameof(CargoEntity.UserId))]
        [MapperIgnoreSource(nameof(CargoEntity.CreateDate))]
        [MapperIgnoreSource(nameof(CargoEntity.UpdateDate))]
        [MapperIgnoreSource(nameof(CargoEntity.State))]
        public partial CargoTransportDto Map(CargoEntity source);

        private List<string> Map(List<CargoImageEntity> source) 
            => source.Select(x => x.Url).ToList();
        
        private ContactDto Map(User source)
            => new(!string.IsNullOrEmpty(source.Organization) ? source.Organization : $"{source.FullName.First} {source.FullName.Second} {source.FullName.Surname}", source.Contact.PhoneViber,
                source.Contact.PhoneTelegram, source.Contact.PhoneWhatsapp, source.Contact.WorkingMode, source.ImageUrl);
    }
}
