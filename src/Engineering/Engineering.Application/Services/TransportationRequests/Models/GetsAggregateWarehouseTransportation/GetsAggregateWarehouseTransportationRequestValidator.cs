namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;

public class GetsAggregateWarehouseTransportationRequestValidator : AbstractValidator<GetsAggregateWarehouseTransportationRequest>
{
    public GetsAggregateWarehouseTransportationRequestValidator()
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
