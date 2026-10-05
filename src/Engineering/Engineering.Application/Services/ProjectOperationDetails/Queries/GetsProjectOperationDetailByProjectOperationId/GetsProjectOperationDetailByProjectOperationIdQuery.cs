using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByProjectOperationId;

public record GetsProjectOperationDetailByProjectOperationIdQuery(
    List<long>? Ids,
    long ProjectOperationId,
    string? PrivateName,
    string? PrivateCode,
    string? FilterData,
    long? EmployerId,
    ProjectOperationDetailStatus? Status,
    List<long>? ContractorIds,
    DateTime? CreateDate,
    DateTime? StartDate,
    DateTime? EndDate,
    List<long>? ServiceInfoIds,
    List<long>? ImplementationAssistantIds,
    List<long>? TechnicalAssistantIds,
    long? CreatorId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailsModel>>>;