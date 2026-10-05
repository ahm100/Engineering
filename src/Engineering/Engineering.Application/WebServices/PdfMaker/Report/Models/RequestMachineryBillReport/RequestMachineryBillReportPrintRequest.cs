namespace Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;
public record RequestMachineryBillReportPrintRequest(
    IEnumerable<RequestMachineryBillReportPrintData>? Data,
    string ReportName = "RequestMachineryBill",
    Dictionary<string, string?>? Parameters = null);