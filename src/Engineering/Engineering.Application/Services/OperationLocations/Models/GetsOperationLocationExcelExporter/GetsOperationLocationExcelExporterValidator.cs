
namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;

public class GetsOperationLocationExcelExporterValidator : AbstractValidator<GetsOperationLocationExcelExporterRequest>
{
    public GetsOperationLocationExcelExporterValidator()
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
