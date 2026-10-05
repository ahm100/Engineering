using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoIdIncludeless;

public record GetsByOperationInfoIdIncludelessQuery(
    long OprationInfoId
    ) : IQuery<DataResult<List<OperationInfoService>>>;