
namespace Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;

public class CreateWarehouseTransportationValidator : AbstractValidator<CreateWarehouseTransportationRequest>
{
    public CreateWarehouseTransportationValidator()
    {
        RuleForEach(oo => oo.PackingIds).IsPositive("پکینگ");
    }
}
