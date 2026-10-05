
namespace Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;

public class GetsMachineriesGroupExcelExporterValidator : AbstractValidator<GetsMachineriesGroupExcelExporterRequest>
{
    public GetsMachineriesGroupExcelExporterValidator()
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
