
namespace Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;

public class GetPackingLogesticDetailValidator : AbstractValidator<GetPackingLogesticDetailRequest>
{
    public GetPackingLogesticDetailValidator()
    {
        RuleFor(oo => oo.PackingId).NotNull().GreaterThanOrEqualTo(1).WithError(TransportationRequestErrors.UnvalidPacking);
    }
}
