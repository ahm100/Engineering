
namespace Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;

public class GetTransportationCargoPalletRequestValidator : AbstractValidator<GetTransportationCargoPalletRequest>
{
    public GetTransportationCargoPalletRequestValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
