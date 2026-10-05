namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;

public class GetSubProjectsValidator : AbstractValidator<GetSubProjectsRequest>
{
    public GetSubProjectsValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);

        RuleFor(oo => oo.Status)
            .IsNullableEnum(GlobalCmts.Status);

        RuleFor(oo => oo.Type)
            .IsNullableEnum(GlobalCmts.Type);
    }
}
