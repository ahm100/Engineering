using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;

namespace Financial.Application.WebServices.PdfMaker.Report.Commands.RequestMachineryBillReport;
public record RequestMachineryBillReportCommand(IEnumerable<RequestMachineryBillReportPrintData>? Data,
    Dictionary<string, string?>? Parameters = null) : ICommand<byte[]>;
