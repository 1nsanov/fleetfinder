using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Cargo;
using FleetFinder.Domain.Users;


namespace FleetFinder.Application.Features.CargoTransport.GetList;

public static partial class GetCargoTransportList
{

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.PageSize).InclusiveBetween(6, 20);
            RuleFor(x => x.SkipCount).InclusiveBetween(0, int.MaxValue);
        }
    }

    
}
