
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetHistoryByProjectOperationDetailId;

public record GetHistoryByProjectOperationDetailIdResponse(
    long ProjectOperationDetailId,
    long? ProjectOperationId,
    string? OperationinfoName,
    long? OperationLocationId,
    string? PublicName,
    string? PublicCode,
    string? privateName,
    string? privateCode,
    long CreatorId,
    string? Creator,
    string? StartDate,
    string? EndDate,
    string? CreateDate,
    List<GetHistoryByProjectOperationDetailIdModel> Data,
    int RowCount
    );
