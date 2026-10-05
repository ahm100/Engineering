
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;

public record GetsSummarizedByProjectOperationIdsModel(
    long Id,
    long ProjectOperationId,
    string? PrivateName,
    string? PrivateCode,
    string? PublicName,
    string? PublicCode,
    string? Description
    );
