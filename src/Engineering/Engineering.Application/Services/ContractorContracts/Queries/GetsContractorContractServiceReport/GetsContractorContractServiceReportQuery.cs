using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractServiceReport;

public record GetsContractorContractServiceReportQuery(
     List<long>? Ids,
     List<long>? CostCenterIds,
     List<long>? ProjectIds,
     List<long>? ProjectOperationIds,
     List<long>? ProjectOperationDetailIds,
     List<long>? ContractorIds,
     List<long>? ServiceInfoIds,
     List<long>? MeasurUnitIds,
     long? ContractTypeId,
     DateTime? StartDate,
     DateTime? EndDate,
     DateTime? FromDate,
     DateTime? ToDate,
     DateTime? FromCreated,
     DateTime? ToCreated,
     ProjectOperationDetailStatus? Status,
     ContractorContractStatus? ContractStatus,
     string? FilterData,
     string? FilterDescription,
     string? FilterServiceInfo,
     string[]? OrderBy,
     int PageIndex,
     int PageSize
    ) : IQuery<DataResult<List<GetsContractorContractServiceReportModel>>>;
