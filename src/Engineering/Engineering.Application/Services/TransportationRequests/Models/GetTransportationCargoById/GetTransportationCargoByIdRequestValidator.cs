
namespace Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;

public class GetTransportationCargoByIdRequestValidator : AbstractValidator<GetTransportationCargoByIdRequest>
{
    public GetTransportationCargoByIdRequestValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
