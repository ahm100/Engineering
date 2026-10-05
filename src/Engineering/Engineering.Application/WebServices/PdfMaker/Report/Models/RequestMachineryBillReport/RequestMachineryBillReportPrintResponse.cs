namespace Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;
public class RequestMachineryBillReportPrintResponse
{
    [JsonProperty("value")]
    public RequestMachineryBillReportPrintResponseModel? Value { get; set; }
    public int StatusCode { get; set; }
}