namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionDocumentModel;

public record ProjectOperationDetailInspectionDocumentRequestModel(
    long? Id,
    string DocumentUrl,
    bool IsDeleted
    );
