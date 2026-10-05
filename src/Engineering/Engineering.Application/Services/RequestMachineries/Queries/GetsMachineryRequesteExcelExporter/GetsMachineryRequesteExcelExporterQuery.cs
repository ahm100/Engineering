using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsMachineryRequesteExcelExporter;

public record GetsMachineryRequesteExcelExporterQuery(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    List<long>? ProjectOperationIds,
    long? MachineriesGroupId,
    long? MachineryId,
    RequestMachineryStatus? Status,
    DateTime? FromDate,
    DateTime? ToDate,
    DateTime? ConfirmedFromDate,
    DateTime? ConfirmedToDate,
    long? CreatorId,
    long? OperatorAppoinmentUserId,
    int? RequestNumber,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestMachinery>>>;
