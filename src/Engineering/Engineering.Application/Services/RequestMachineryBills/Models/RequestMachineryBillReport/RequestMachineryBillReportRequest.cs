namespace Financial.Application.AccountingDocuments.Models.PrintAccountingDocument;

public record RequestMachineryBillReportRequest(List<long> Ids) : IHttpRequest;