using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;

namespace Financial.Infra.Providers.PdfMaker;
public interface IPdfMakerProvider
{

    /// <summary>
    /// ایجاد فایل پی دی اف پیش فاکتور
    /// </summary>
    [Post("/v1/Report/RequestMachineryBillReport/")]
    Task<ApiResponse<Stream>?> RequestMachineryBillReport(
        [Body] RequestMachineryBillReportPrintRequest request, CT ct);
}
