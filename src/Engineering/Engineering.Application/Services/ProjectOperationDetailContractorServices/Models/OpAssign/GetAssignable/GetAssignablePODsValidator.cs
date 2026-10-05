namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;

public class GetAssignablePODsValidator
    : AbstractValidator<GetAssignablePODsRequest>
{
    public GetAssignablePODsValidator()
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