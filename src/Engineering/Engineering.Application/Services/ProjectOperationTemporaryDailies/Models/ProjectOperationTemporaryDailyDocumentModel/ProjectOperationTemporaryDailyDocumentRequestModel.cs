namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyDocumentModel;

public record ProjectOperationTemporaryDailyDocumentRequestModel(
    long? Id,
    string DocumentUrl,
    bool IsDeleted
    );
