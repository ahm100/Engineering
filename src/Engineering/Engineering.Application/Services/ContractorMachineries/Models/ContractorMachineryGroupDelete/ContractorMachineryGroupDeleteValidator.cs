
namespace Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryGroupDelete;

public class ContractorMachineryGroupDeleteValidator : AbstractValidator<ContractorMachineryGroupDeleteRequest>
{
    public ContractorMachineryGroupDeleteValidator()
    {
        RuleFor(c => c.Ids).NotEmpty().WithError(GlobalErrors.IdsIsEmpty).NotNull().WithError(GlobalErrors.IdsIsNull);
        RuleForEach(c => c.Ids).GreaterThan(0).WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}
