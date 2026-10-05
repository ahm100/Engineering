
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetDeductionAmountByProjectOperationDetailId;

public class GetDeductionAmountByProjectOperationDetailIdQueryValidator : AbstractValidator<GetDeductionAmountByProjectOperationDetailIdQuery>
{
    public GetDeductionAmountByProjectOperationDetailIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailDeductionErrors.ProjectOperationDetailIdIsEmpty)
           .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}