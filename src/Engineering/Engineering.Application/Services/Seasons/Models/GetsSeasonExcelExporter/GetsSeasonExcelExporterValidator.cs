
namespace Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;

public class GetsSeasonExcelExporterValidator : AbstractValidator<GetsSeasonExcelExporterRequest>
{
    public GetsSeasonExcelExporterValidator()
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
