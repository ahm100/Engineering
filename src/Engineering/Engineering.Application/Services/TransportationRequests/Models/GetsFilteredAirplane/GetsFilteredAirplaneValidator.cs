
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;

public class GetsFilteredAirplaneValidator : AbstractValidator<GetsFilteredAirplaneRequest>
{
    public GetsFilteredAirplaneValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
