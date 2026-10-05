namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;

public class GetProjectCostCenterRequestsValidator : AbstractValidator<GetProjectCostCenterRequestsRequest>
{
    public GetProjectCostCenterRequestsValidator()
    {
        When(oo => oo.ProjectId != null, () =>
        {
            RuleFor(oo => oo.ProjectId)
                .IsPositiveWithNullableInput(GlobalCmts.ProjectId);
        });

        When(oo => oo.Status != null, () =>
        {
            RuleFor(oo => oo.Status)
            .IsNullableEnum(GlobalCmts.Status);
        });

        RuleFor(oo => oo.PageIndex)
            .PageIndexOne(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}