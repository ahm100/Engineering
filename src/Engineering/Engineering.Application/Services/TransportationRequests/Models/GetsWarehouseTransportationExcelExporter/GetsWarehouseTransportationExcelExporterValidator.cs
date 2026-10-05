namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelExporter;

public class GetsWarehouseTransportationExcelExporterValidator : AbstractValidator<GetsWarehouseTransportationExcelExporterRequest>
{
    public GetsWarehouseTransportationExcelExporterValidator()
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
