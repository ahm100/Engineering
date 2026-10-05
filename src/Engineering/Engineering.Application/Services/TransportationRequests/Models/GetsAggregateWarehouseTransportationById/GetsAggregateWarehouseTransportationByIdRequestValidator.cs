namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;

public class GetsAggregateWarehouseTransportationByIdRequestValidator : AbstractValidator<GetsAggregateWarehouseTransportationByIdRequest>
{
    public GetsAggregateWarehouseTransportationByIdRequestValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
