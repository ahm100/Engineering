using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoId;

public record GetsByOperationInfoIdQuery(
    long OprationInfoId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoService>>>;