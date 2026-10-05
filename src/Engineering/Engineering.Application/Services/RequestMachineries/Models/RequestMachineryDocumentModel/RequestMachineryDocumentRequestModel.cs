namespace Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;

public record RequestMachineryDocumentRequestModel(
    long? Id,
    string DocumentUrl,
    bool IsDeleted
    );
