namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;

public class GetDailyHistoriesExcelExporterValidator : AbstractValidator<GetDailyHistoriesExcelExporterRequest>
{
    public GetDailyHistoriesExcelExporterValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(DailyProjectOperationErrors.InValidProjectOperationDetailId);
    }
}
