
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Queries.GetsProjectOperationDetailDeductionByIds;

public class GetsProjectOperationDetailDeductionByIdsValidator : AbstractValidator<GetsProjectOperationDetailDeductionByIdsQuery>
{
    public GetsProjectOperationDetailDeductionByIdsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().NotEmpty().WithError(GlobalErrors.IdsIsEmpty);
    }
}
