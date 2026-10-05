using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetFilteredProjectOperationDetailInspections;

public record GetFilteredProjectOperationDetailInspectionsQuery(
                                                 long? CostCenterId,
                                                 long? ProjectId,
                                                 long? OperationInfoId,
                                                 long? OperationLocationId,
                                                 long? ProjectOperationId,
                                                 long? ProjectOperationdetailId,
                                                 DateTime? FromDate,
                                                 DateTime? ToDate,
                                                 string? FilterData,
                                                 string[]? OrderBy,
                                                 int PageIndex,
                                                 int PageSize) : IQuery<DataResult<List<ProjectOperationDetailInspection>>>;
