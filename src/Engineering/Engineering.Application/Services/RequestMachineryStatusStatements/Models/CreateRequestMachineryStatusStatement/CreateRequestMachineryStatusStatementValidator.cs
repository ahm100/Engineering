
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.CreateRequestMachineryStatusStatement;

public class CreateRequestMachineryStatusStatementValidator : AbstractValidator<CreateRequestMachineryStatusStatementRequest>
{
    public CreateRequestMachineryStatusStatementValidator()
    {
        RuleFor(c => c.ContractorId)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidContractorId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
