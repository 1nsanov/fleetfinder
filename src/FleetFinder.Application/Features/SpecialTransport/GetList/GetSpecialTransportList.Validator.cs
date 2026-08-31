using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Special;
using FleetFinder.Domain.Users;


namespace FleetFinder.Application.Features.SpecialTransport.GetList;

public static partial class GetSpecialTransportList
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
