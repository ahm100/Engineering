namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetDeductionAmountByProjectOperationDetailId;

public class GetDeductionAmountByProjectOperationDetailIdValidator : AbstractValidator<GetDeductionAmountByProjectOperationDetailIdRequest>
{
    public GetDeductionAmountByProjectOperationDetailIdValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailDeductionErrors.ProjectOperationDetailIdIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
