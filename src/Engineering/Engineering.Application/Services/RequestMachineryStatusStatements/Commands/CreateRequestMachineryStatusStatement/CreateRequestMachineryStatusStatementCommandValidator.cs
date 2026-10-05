
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatement;

public class CreateRequestMachineryStatusStatementCommandValidator : AbstractValidator<CreateRequestMachineryStatusStatementCommand>
{
    public CreateRequestMachineryStatusStatementCommandValidator()
    {
        RuleFor(c => c.ContractorId)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidContractorId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(c => c.TotalRequestedCount)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidTotalRequestedCount);

        RuleFor(c => c.TotalFinalPrice)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidTotalFinalPrice);
    }
}
