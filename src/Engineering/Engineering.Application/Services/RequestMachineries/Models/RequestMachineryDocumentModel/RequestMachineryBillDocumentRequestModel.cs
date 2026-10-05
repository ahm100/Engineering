namespace Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;

public record RequestMachineryBillDocumentRequestModel(
    long? Id,
    string BillUrl,
    bool? IsDeleted
    );
