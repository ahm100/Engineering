namespace Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;

public class GetsCategoryExcelExporterValidator : AbstractValidator<GetsCategoryExcelExporterRequest>
{
    public GetsCategoryExcelExporterValidator()
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