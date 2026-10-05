
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;

public class GetProjectOperationDetailDeductionByIdValidator : AbstractValidator<GetProjectOperationDetailDeductionByIdRequest>
{
    public GetProjectOperationDetailDeductionByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectOperationDetailDeductionErrors.IdIsEmpty);
    }
}
