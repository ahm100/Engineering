
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;

public class GetRequestMachineryStatusStatementByIdValidator : AbstractValidator<GetRequestMachineryStatusStatementByIdRequest>
{
    public GetRequestMachineryStatusStatementByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.RequestMachineryStatusStatementWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

    }
}
