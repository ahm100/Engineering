namespace Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;

public class GetsBranchExcelExporterValidator : AbstractValidator<GetsBranchExcelExporterRequest>
{
    public GetsBranchExcelExporterValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}