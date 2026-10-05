namespace Financial.Application.AccountingDocuments.Models.PrintAccountingDocument;

public class RequestMachineryBillReportRequestValidator : AbstractValidator<RequestMachineryBillReportRequest>
{
    public RequestMachineryBillReportRequestValidator()
    {
        RuleFor(v => v.Ids).NotEmpty().WithError(RequestMachineryBillErrors.InValidRequestMachinery);
    }
}