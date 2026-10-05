
namespace Engineering.Application.Services.ContractorStatusStatements.Models.UpdateContractorStatusStatement;

public class UpdateContractorStatusStatementValidator : AbstractValidator<UpdateContractorStatusStatementRequest>
{
    public UpdateContractorStatusStatementValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(CSSErrors.InValidContractor)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

    }
}
