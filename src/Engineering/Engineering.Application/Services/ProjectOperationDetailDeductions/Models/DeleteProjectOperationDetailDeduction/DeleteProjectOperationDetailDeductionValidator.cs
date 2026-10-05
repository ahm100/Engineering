
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.DeleteProjectOperationDetailDeduction;

public class DeleteProjectOperationDetailDeductionValidator : AbstractValidator<DeleteProjectOperationDetailDeductionRequest>
{
    public DeleteProjectOperationDetailDeductionValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ContractorServiceErrors.IdIsEmpty);
    }
}
