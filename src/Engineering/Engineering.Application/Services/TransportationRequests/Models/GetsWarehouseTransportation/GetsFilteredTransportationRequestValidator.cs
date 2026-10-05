
namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;

public class GetsWarehouseTransportationRequestValidator : AbstractValidator<GetsWarehouseTransportationRequest>
{
    public GetsWarehouseTransportationRequestValidator()
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
