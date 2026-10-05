namespace Engineering.Application.Services.ProcesVerbal.Queries.GetProcesVerbals;

public class GetProcesVerbalsQueryValidator : AbstractValidator<GetProcesVerbalsQuery>
{
    public GetProcesVerbalsQueryValidator()
    {
        RuleFor(oo => oo.TargetId)
            .IsOptionalPositive(GlobalCmts.Id);

        RuleFor(oo => oo.ProjectId)
            .IsOptionalPositive(GlobalCmts.ProjectId);

        RuleFor(oo => oo.ContractId)
            .IsOptionalPositive(GlobalCmts.ContractId);

        RuleFor(oo => oo.Type)
            .IsNullableEnum(ProcesVerbalCmts.Type);

        RuleFor(oo => oo.Title)
            .MaximumLength(250)
            .WithError(GlobalErrors.RequiredMaxLength(GlobalCmts.Title, 250))
            .When(oo => oo.Title != null);

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