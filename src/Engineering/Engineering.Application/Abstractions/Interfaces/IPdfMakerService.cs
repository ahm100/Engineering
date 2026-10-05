using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;

namespace Financial.Application.Abstractions.Interfaces;
public interface IPdfMakerService
{
    Task<Result<RequestMachineryBillReportPrintResponseModel?>> RequestMachineryBillReport(
        RequestMachineryBillReportPrintRequest request,
        CT ct);
}
