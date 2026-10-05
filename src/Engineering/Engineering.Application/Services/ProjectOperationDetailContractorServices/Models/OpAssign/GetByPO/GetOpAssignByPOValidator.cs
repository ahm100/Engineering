namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;

public class GetOpAssignByPOValidator
    : AbstractValidator<GetOpAssignByPORequest>
{
    public GetOpAssignByPOValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(ProjectOperationErrors.IdIsEmpty);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}