
namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;

public class GetsProjectTypeExcelExporterValidator : AbstractValidator<GetsProjectTypeExcelExporterRequest>
{
    public GetsProjectTypeExcelExporterValidator()
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
