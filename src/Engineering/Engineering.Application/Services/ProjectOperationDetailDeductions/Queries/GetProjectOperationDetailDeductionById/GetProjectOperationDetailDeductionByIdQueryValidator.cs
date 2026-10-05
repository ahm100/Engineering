
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetProjectOperationDetailDeductionById;

public class GetProjectOperationDetailDeductionByIdQueryValidator : AbstractValidator<GetProjectOperationDetailDeductionByIdQuery>
{
    public GetProjectOperationDetailDeductionByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectOperationDetailDeductionErrors.IdIsEmpty);
    }
}