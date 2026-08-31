using FleetFinder.Application.Common.FeatureModels;
using FleetFinder.Domain.Transport.Passenger;
using FleetFinder.Domain.Users;


namespace FleetFinder.Application.Features.PassengerTransport.GetList;

public static partial class GetPassengerTransportList
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
