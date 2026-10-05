
namespace Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;

public class GetsFilteredRequesterRequestValidator : AbstractValidator<GetsFilteredRequesterRequest>
{
    public GetsFilteredRequesterRequestValidator()
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
